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
        /// PT - Construtor com a Matriz de Pesos das observações
        /// EN - Constructor with Wheight Observation Matrix
        /// </summary>
        /// <param name="nParamIn">number of unknown parameters</param>
        /// <param name="nObsIn">number of observations</param>
        /// <param name="lIn">Observation Matrix (Vector)</param>
        /// <param name="aIn">1st Configuration Matrix</param>
        /// <param name="wIn">Observation Wheight Matrix</param>
        /// <param name="varIn">A Priori Reference Variance</param>
        public LinearParametric(int nParamIn, int nObsIn, Matrix lIn, Matrix aIn, Matrix wIn, double varIn)
        {
            nParam = nParamIn;
            nObs = nObsIn;
            l = lIn;
            a = aIn;
            w = wIn;
            var = varIn;
            if (w != null)
                GenerateCovarianceObs();
            else
                throw new MissingFieldException("Wheight obs matrix not defined.");
        }

        /// <summary>
        /// PT - Construtor com a Matriz Variancias e Covariancias das observações
        /// EN - Constructor with Variance Observation Matrix
        /// </summary>
        /// <param name="nParamIn">number of unknown parameters</param>
        /// <param name="nObsIn">number of observations</param>
        /// <param name="lIn">Observation Matrix (Vector)</param>
        /// <param name="aIn">1st Configuration Matrix</param>
        /// <param name="qllIn">Observations Var Matrix</param>
        /// <param name="varIn">A Priori Reference Variance</param>
        public LinearParametric(int nParamIn, int nObsIn, Matrix lIn, Matrix aIn, Matrix qllIn, double varIn)
        {
            nParam = nParamIn;
            nObs = nObsIn;
            l = lIn;
            a = aIn;
            qll = qllIn;
            var = varIn;
            if (qllIn != null)
                GenerateWeights();
            else
                throw new MissingFieldException("Var matrix not defined.");
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

        #region metodos auxiliares

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

        #endregion 

        #region sets and gets

        /// <summary>
        /// (EN) get and set the number of parameters
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
        /// (EN) get and set the number of observations
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
        /// (EN) get and set the matrix (vector) of observations
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
        /// (EN) get and set the covariance matrix of the observations
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
        /// (EN) get and set the weight matrix
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
        ///(EN) get and set the design matrix
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
        /// (EN) get and set the normal matrix
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
        /// (EN) get and set the inverse of normal matrix
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
        /// (EN) get and set the a priori variance factor
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
        /// (EN) get and set a posteriori variance factor [vanicek, 1982]
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
        /// (EN) get and set the residuals
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
        /// (EN) get and set adjusted parameters
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
        /// (EN) get and set the covariance matrix of the adjusted parameters 
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
        /// (EN) get and set adjusted observations
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

        #endregion
    }
}
