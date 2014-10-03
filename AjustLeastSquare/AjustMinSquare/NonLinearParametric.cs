using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace AjustLeastSquare
{
    public class NonLinearParametric
    {
        //numero de parametros, numero de observações
        private int nParam, nObs;
        //vector de observacões, primeira matriz de configuração
        private Matrix l, a, aT;
        //matriz de var e co-var das obs, vector de correcções aos param, matriz de pesos, vector de fecho
        private Matrix qll, delta, w, f;
        //variancia de referencia a priori e posteriori
        private double var, varPos;
        //1ª matriz das eq normais, 2ª matriz de eq normais, vector de residuos, vector parametros
        private Matrix n, nInv, v, vT, x;
        //matriz obs ajustadas, matriz de variancias e co-variancias dos param, Matriz ajustada das V&C dos parametros ajustados
        private Matrix lAjs, qxx, qxxAjs;
        //Graus de liberdade
        private int df;
        //Iterações
        private int iterations = 0;

        private Matrix qvv, qllAjs, qvvAjs;

        //delegates
        public delegate Matrix preencherA(Matrix a, Matrix l, Matrix x);        
        public delegate Matrix preencherF(Matrix f, Matrix l, Matrix x);

        /// <summary>
        /// Construtor com a Matriz de Pesos ou Matriz de variancias e covariancias das observações
        /// </summary>
        /// <param name="nParamIn">número de parametros</param>
        /// <param name="nObsIn">número de observações</param>
        /// <param name="xIn">Matriz (vector) dos parâmetros desconhecidos</param>
        /// <param name="lIn">Matriz (vector) das observações</param>
        /// <param name="aIn">Primeira Matriz de configuração</param>
        /// <param name="bIn">Segunda Matriz de configuração</param>
        /// <param name="wIn">Matriz dos pesos das observações (pode ser null)</param>
        /// <param name="qllIn">Matriz de VeC das observações (pode ser null)</param>
        /// <param name="varIn">Variancia de referencia a priori</param>
        public NonLinearParametric(int nParamIn, int nObsIn, Matrix xIn, Matrix lIn, Matrix wIn, Matrix qllIn, double varIn)
        {
            nParam = nParamIn;
            nObs = nObsIn;
            x = xIn;
            l = lIn;
            w = wIn;
            qll = qllIn;
            var = varIn;
            GenerateDelta();
            if (w != null)
                GenerateCovarianceObs();
            else if (qllIn != null)
                GenerateWeights();
            else
                throw new MissingFieldException("Matriz de var e co-var ou matriz de pesos das obs não definida");
        }

        /// <summary>
        /// (EN) Implementation of the adjustment.
        /// (PT) Calculo do ajustamento.
        /// </summary>
        /// <param name="difA">método que preenche a 1ª matriz de configuração</param>
        /// <param name="difF">método que preenche a matriz de fecho</param>
        public void Compute(preencherA difA = null, preencherF difF = null)
        {
            for (iterations = 0; (delta.Norm1() > Math.Pow(10, -6)) & iterations < 10; iterations++)
            {
                a = difA != null ? difA(a, l, x) : a;
                f = difF != null ? difF(f, l, x) : f;
                
                //transpostas das matrizes a
                aT = a.Clone();
                aT.Transpose();

                //2ª Matriz das equações normais
                n = aT * w * a;
                //ShowMatrix(n, "{0,20:0.0000000}");
                //Inversa da 2ª Matriz das equações normais
                nInv = n.Inverse();
                ShowMatrix(w);
                ShowMatrix(aT);
                ShowMatrix(nInv);

                //Vector de correcção ao parametros desconhecidos
                delta = (-1 * nInv) * aT;
                delta = delta * w * f;

                //vector dos parametros desconhecidos
                x = x + delta;
            }
            v = a * delta + f;

            vT = v.Clone();
            vT.Transpose();

            //variancia de referencia a posteriori
            Matrix apo;
            apo = vT * w * v;
            varPos = apo[0, 0] / DegreeOfFreedom;
        }

        /// <summary>
        /// Gera a matriz de pesos a partir da matriz de variancias e covariancias
        /// </summary>
        private void GenerateWeights()
        {
            //Pl = Cl.Inverse();            
            w = qll.InvDiagMatrix();
            w = var * w;
        }

        /// <summary>
        /// Gera a matriz de variancias e covariancias a partir da matriz de pesos
        /// </summary>
        private void GenerateCovarianceObs()
        {
            //qll -> Covariance matrix of the observations            
            qll = w.InvDiagMatrix();
            qll = var * qll;
        }

        private void GenerateDelta()
        {
            delta = Matrix.CreateFromColumns(new List<Vector>(1) { Vector.Ones(nParam) });
        }

        /// <summary>
        /// (EN) define and return the number of parameters
        /// (PT) define e retorna o número de parametros
        /// </summary>
        public int NParameters
        {
            get
            {
                return nParam;
            }
            set
            {
                nParam = value;
            }
        }

        /// <summary>
        /// (EN) define and return the number of observations
        /// (PT) define e retorna o número de observações
        /// </summary>
        public int NObservations
        {
            get
            {
                return nObs;
            }
            set
            {
                nObs = value;
            }
        }

        /// <summary>
        /// (EN) define and return the matrix (vector) of observations
        /// (PT) define e retorna a matriz (vetor) de observações
        /// </summary>
        public Matrix Observations
        {
            get
            {
                return l;
            }
            set
            {
                l = value;
            }
        }

        /// <summary>
        /// (EN) define and return the covariance matrix of the observations
        /// (PT) define e retorna a Matriz de variancias e covariancias das observações
        /// </summary>
        public Matrix CovariancesObs
        {
            get
            {
                return qll;
            }
            set
            {
                qll = value;
            }
        }

        /// <summary>
        /// (EN) define and return the covariance matrix of the adjusted observations
        /// (PT) define e retorna a Matriz de variancias e covariancias das observações ajustadas
        /// </summary>
        public Matrix CovariancesAdjustedObs
        {
            get
            {
                qllAjs = qll + qvvAjs;
                return qllAjs;
            }
            set
            {
                qllAjs = value;
            }
        }

        /// <summary>
        /// (EN) define and return the weight matrix
        /// (PT) define e retorna a Matriz de pesos das observações
        /// </summary>
        public Matrix WeightsObs
        {
            get
            {
                return w;
            }
            set
            {
                w = value;
            }
        }

        /// <summary>
        ///(EN) define and return the closer matrix
        ///(PT) define e retorna a Matriz de fecho
        /// </summary>
        public Matrix CloserMatrix
        {
            get
            {
                return f;
            }
            set
            {
                f = value;
            }
        }

        /// <summary>
        ///(EN) define and return the first design matrix
        ///(PT) define e retorna a primeira Matriz de configuração
        /// </summary>
        public Matrix FstDesignMatrix
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }

        
        /// <summary>
        /// (EN) define and return the normal matrix (aT * Pl * a)
        /// (PT) define e retorna a Matriz Normal (aT * Pl * a)
        /// </summary>
        public Matrix NormalMatrix
        {
            get
            {
                return n;
            }
            set
            {
                n = value;
            }
        }

        /// <summary>
        /// (EN) define and return the inverse normal matrix (aT * Pl * a)
        /// (PT) define e retorna a inversa da Matriz Normal (aT * Pl * a)
        /// </summary>
        public Matrix InvNormalMatrix
        {
            get
            {
                return nInv;
            }
            set
            {
                nInv = value;
            }
        }

        /// <summary>
        /// (EN) define and return the a priori variance factor
        /// (PT) define e retorna a variancia a priori
        /// </summary>
        public double VarApriori
        {
            get
            {
                return var;
            }
            set
            {
                var = value;
            }
        }

        /// <summary>
        /// (EN) define and return a posteriori variance factor [vanicek, 1982]
        /// (PT) define e retorna a variancia aposteriori
        /// </summary>
        public double VarAposteriori
        {
            get
            {
                return varPos;
            }
            set
            {
                varPos = value;
            }
        }

        /// <summary>
        /// (EN) define and return the residuals
        /// (PT) define e retorna a Matriz (vector) dos residuos
        /// </summary>
        public Matrix Residuals
        {
            get
            {
                return v;
            }
            set
            {
                v = value;
            }
        }

        /// <summary>
        /// (EN) define and return the parameters correction
        /// (PT) define e retorna a Matriz (vector) de correcções aos parametros
        /// </summary>
        public Matrix Delta
        {
            get
            {
                return delta;
            }
            set
            {
                delta = value;
            }
        }

        /// <summary>
        /// (EN) define and return adjusted parameters
        /// (PT) define e retorna a "Matriz" dos parametros ajustados
        /// </summary>
        public Matrix AjustedParam
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>
        /// (EN) define and return the covariance matrix of the adjusted residuals 
        /// (PT) define e retorna a Matriz das variancias e covariancias dos resíduos ajustados
        /// </summary>
        public Matrix CovariancesAjustedResiduals
        {
            get
            {
                qvv = var * (qll * (1 / var) - a * nInv * aT); //qll * (1 / var) = Pl_Inversa
                return qvv;
            }
            set
            {
                qvv = value;
            }
        }

        /// <summary>
        /// (EN) define and return the covariance matrix of the adjusted parameters (qxx - without var apriori)
        /// (PT) define e retorna a Matriz das variancias e covariancias dos parametros ajustados (qxx - sem factor de var apriori)
        /// </summary>
        public Matrix CovariancesAjustedParam
        {
            get
            {
                qxx =  nInv;
                return qxx;
            }
            set
            {
                qxx = value;
            }
        }

        /// <summary>
        /// (EN) define and return adjusted observations
        /// (PT) define e retorna a Matriz (vector) das observações ajustadas
        /// </summary>
        public Matrix AjustedObs
        {
            get
            {
                lAjs = l + v;
                return lAjs;
            }
            set
            {
                lAjs = value;
            }
        }

        /// <summary>
        /// (EN) return and define the adjusted covariance matrix of the adjusted residuals 
        /// (PT) retorna a "Matriz" ajustada das variâncias e co-variâncias dos resíduos ajustados
        /// </summary>
        public Matrix AjustedCovariancesAjustedResidual
        {
            get
            {
                qvvAjs = varPos * (qll * (1 / var) - a * nInv * aT);
                return qvvAjs;
            }
            set
            {
                qvvAjs = value;
            }
        }

        /// <summary>
        /// (EN) return and define the adjusted covariance matrix of the adjusted parameters 
        /// (PT) retorna a "Matriz" ajustada das variâncias e co-variâncias dos parametros ajustados
        /// </summary>
        public Matrix AjustedCovariancesAjustedParam
        {
            get
            {
                qxxAjs = varPos * nInv;
                return qxxAjs;
            }
            set
            {
                qxxAjs = value;
            }
        }

        public int DegreeOfFreedom
        {
            get
            {
                df = nObs - nParam;
                return df;
            }
            set
            {
                df = value;
            }
        }

        /// <summary>
        /// PT - numero de iterações do ciclo de ajustamento
        /// EN - number of iterations in ajustment
        /// </summary>
        public int NumIterations
        {
            get { return iterations; }
        }

        #region *** temporarios ***
        // Display the array of separated strings.
        public static void Show(string[] entries)
        {
            Console.WriteLine("The return value contains these {0} elements:", entries.Length);
            foreach (string entry in entries)
            {
                Console.Write("<{0}>", entry);
            }
            Console.Write("\n\n");
        }

        public void ShowMatrix(Matrix A)
        {
            for (int i = 0; i < A.RowCount; i++)
            {
                for (int j = 0; j < A.ColumnCount; j++)
                {
                    Console.Write("{0,9:0.000000}  ", A[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public void ShowMatrix(Matrix A, String formato)
        {
            for (int i = 0; i < A.RowCount; i++)
            {
                for (int j = 0; j < A.ColumnCount; j++)
                {
                    Console.Write(formato, A[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        #endregion*** temporarios ***
    }
}
