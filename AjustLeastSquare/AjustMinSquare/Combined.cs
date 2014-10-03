using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace AjustLeastSquare
{
    public class Combined
    {
        //numero de parametros, numero de observações
        private int nParam, nObs;
        //vector de observacões, primeira matriz de configuração, segunda matriz de configuracao
        private Matrix l, a, aT, b, bT;
        //matriz de var e co-var das obs, vector de correcções aos param, matriz de pesos, vector de fecho
        private Matrix qll, delta, w, f;
        //variancia de referencia a priori e posteriori
        private double var, varPos;
        //1ª matriz das eq normais, 2ª matriz de eq normais, vector de residuos, vector parametros
        private Matrix n, nInv, m, mInv, v, vT, x;
        //matriz obs ajustadas, matriz de variancias e co-variancias dos param, Matriz ajustada das V&C dos parametros ajustados
        private Matrix lAjs, qxx, qxxAjs;

        private Matrix qvv, qllAjs, qvvAjs;

        //delegates
        public delegate Matrix preencherA(Matrix a, Matrix l, Matrix x);
        public delegate Matrix preencherB(Matrix b, Matrix l, Matrix x);
        public delegate Matrix preencherF(Matrix f, Matrix l, Matrix x);

        /// <summary>
        /// Construtor com a Matriz de Pesos ou Matriz de variancias e covariancias das observações
        /// </summary>
        /// <param name="nParamIn">número de parametros</param>
        /// <param name="nObsIn">número de observações</param>
        /// <param name="lIn">Matriz (vector) das observações</param>
        /// <param name="aIn">Primeira Matriz de configuração</param>
        /// <param name="bIn">Segunda Matriz de configuração</param>
        /// <param name="wIn">Matriz dos pesos das observações (pode ser null)</param>
        /// <param name="qllIn">Matriz de VeC das observações (pode ser null)</param>
        /// <param name="varIn">Variancia de referencia a priori</param>
        public Combined(int nParamIn, int nObsIn, Matrix lIn, Matrix wIn, Matrix qllIn, double varIn)
        {
            nParam = nParamIn;
            nObs = nObsIn;
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
        /// (EN) Implementation of the adjustment.
        /// (PT) Calculo do ajustamento.
        /// </summary>
        /// <param name="difA">método que preenche a 1ª matriz de configuração (null se não mudar no ciclo)</param>
        /// <param name="difB">método que preenche a 2ª matriz de configuração (null se não mudar no ciclo)</param>
        /// <param name="difF">método que preenche a matriz de fecho (null se não mudar no ciclo)</param>
        public void Compute(preencherA difA = null, preencherB difB = null, preencherF difF = null)
        {
            //numero de graus de liberdade
            int df = nObs - nParam;

            while (delta.Norm1() > Math.Pow(10, -6))
            {
                a = difA != null ? difA(a, l, x) : a;
                b = difB != null ? difB(b, l, x) : b;
                f = difF != null ? difF(f, l, x) : f;

                //transpostas das matrizes a e b
                bT = b.Clone();
                bT.Transpose();
                aT = a.Clone();
                aT.Transpose();

                //1ª Matriz das equações normais
                m = b * qll * bT;

                //Inversa da 1ª Matriz das equações normais
                mInv = m.Inverse();

                //2ª Matriz das equações normais
                n = aT * mInv * a;

                //Inversa da 2ª Matriz das equações normais
                nInv = n.Inverse();

                //Vector de correcção ao parametros desconhecidos
                delta = (-1 * nInv) * aT;
                delta = delta * mInv * f;

                //vector dos parametros desconhecidos
                x = x + delta;
            }

            Matrix k_;
            k_ = mInv * (a * delta + f);
            v = -1 * qll * bT * k_;

            vT = v.Clone();
            vT.Transpose();

            //variancia de referencia a posteriori
            Matrix apo;
            apo = vT * w * v;
            varPos = apo[0, 0] / df;
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
        ///(EN) define and return the second design matrix
        ///(PT) define e retorna a segunda Matriz de configuração
        /// </summary>
        public Matrix SndDesignMatrix
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        /// <summary>
        /// (EN) define and return the normal matrix (aT * inv [b * qll * bT] * a)
        /// (PT) define e retorna a Matriz Normal (aT * inv [b * qll * bT] * a)
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
                qvv = var * qll * bT * mInv * (b * qll - a * nInv * aT * mInv * b * qll);
                return qvv;
            }
            set
            {
                qvv = value;
            }
        }

        /// <summary>
        /// (EN) define and return the covariance matrix of the adjusted parameters 
        /// (PT) define e retorna a Matriz das variancias e covariancias dos parametros ajustados
        /// </summary>
        public Matrix CovariancesAjustedParam
        {
            get
            {
                qxx = var * nInv;
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
                qvvAjs = varPos * qll * bT * mInv * (b * qll * -a * nInv * aT * mInv * b * qll);
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
    }
}
