using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;


namespace AjustLeastSquare
{
    public class LinearParametric
    {
        //numero de parametros, numero de observações
        private int nParam, nObs;
        //vector de observacões, matriz de configuração, variancia de referencia a posteriori
        private Matrix l, a, qll, w;
        //variancia de referencia a posteriori
        private double var, varPos;
        //matriz de pesos, vector de residuos, matriz de variacias e co-variancias
        private Matrix n, nInv, v, qxx, x, lAjs, qxxAjs;

        /// <summary>
        /// Construtor com a Matriz de Pesos ou Matriz de variancias e covariancias das observações
        /// </summary>
        /// <param name="nParamIn">número de parametros</param>
        /// <param name="nObsIn">número de observações</param>
        /// <param name="lIn">Matriz (vector) das observações</param>
        /// <param name="aIn">Matriz de configuração</param>
        /// <param name="wIn">Matriz dos pesos das observações</param>
        /// <param name="qllIn">Matriz de VeC das observações</param>
        /// <param name="varIn">Variancia de referencia a priori</param>
        public LinearParametric(int nParamIn, int nObsIn, Matrix lIn, Matrix aIn, Matrix wIn, Matrix qllIn, double varIn)
        {
            nParam = nParamIn;
            nObs = nObsIn;
            l = lIn;
            a = aIn;
            w = wIn;
            qll = qllIn;
            var = varIn;
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
        public void GenerateWeights()
        {
            //Pl = Cl.Inverse();            
            w = qll.InvDiagMatrix();
            w = var * w;
        }

        /// <summary>
        /// Gera a matriz de variancias e covariancias a partir da matriz de pesos
        /// </summary>
        public void GenerateCovarianceObs()
        {
            //qll -> Covariance matrix of the observations            
            qll = w.InvDiagMatrix();
            qll = var * qll;
        }

        /// <summary>
        /// (EN) Implementation of the adjustment.
        /// (PT) Calculo do ajustamento.
        /// </summary>
        public void Compute()
        {
            //numero de graus de liberdade
            int df = nObs - nParam;

            //trasposta da matriz a
            Matrix aT;
            aT = a.Clone();
            aT.Transpose();

            //Matriz das equações normais
            n = aT * w * a;

            //Matriz inversa da matriz das equações normais
            nInv = n.Inverse();

            //parametros ajustados
            x = nInv * aT * w * l;

            //residuos
            v = a * x - l;

            //obsevacoes ajustadas
            lAjs = l + v;

            //Matriz de V&C dos parametros ajustados
            qxx = var * nInv;

            //transposta dos residuos
            Matrix vT;
            vT = v.Clone();
            vT.Transpose();

            //variancia de referencia a posteriori
            Matrix apo;
            apo = vT * w * v;
            varPos = apo[0, 0] / df;

            //Matriz ajustada das V&C dos parametros ajustados
            qxxAjs = varPos * nInv;

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
        ///(EN) define and return the design matrix
        ///(PT) define e retorna a Matriz de configuração
        /// </summary>
        public Matrix DesignMatrix
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
        /// (EN) define and return the normal matrix
        /// (PT) define e retorna a Matriz Normal
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
        /// (EN) define and return the inverse of normal matrix
        /// (PT) define e retorna a inversa da Matriz Normal
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
        /// (EN) define and return the covariance matrix of the adjusted parameters 
        /// (PT) define e retorna a Matriz das variancias e covariancias dos parametros ajustados
        /// </summary>
        public Matrix CovariancesAjustedParam
        {
            get
            {
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
                return lAjs;
            }
            set
            {
                lAjs = value;
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
                return qxxAjs;
            }
            set
            {
                qxxAjs = value;
            }
        }

    }
}
