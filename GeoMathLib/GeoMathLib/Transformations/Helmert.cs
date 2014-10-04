using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCoordinates.Seed;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;
using MathNet.Numerics.LinearAlgebra;

namespace GeoMathLib.Transformations
{
    public class Helmert
    {
        private double dx, dy, dz, w, f, k, mu;
        private double vdx, vdy, vdz, vw, vf, vk, vmu;
        private int epoca0, epocaFinal;        
        int t, t0;
        Matrix transl, vTransl, rot, vRot;

        /// <summary>
        /// Definição da Transformação de Helmert 7 Parâmetros
        /// </summary>
        /// <param name="dxIn">translacção em x (metros)</param>
        /// <param name="dyIn">translacção em y (metros)</param>
        /// <param name="dzIn">translacção em z (metros)</param>
        /// <param name="wIn">rotação em x (rad)</param>
        /// <param name="fIn">rotação em y (rad)</param>
        /// <param name="kIn">rotação em z (rad)</param>
        /// <param name="muIn">factor de escala</param>
        public Helmert(double dxIn, double dyIn, double dzIn, double wIn, double fIn, double kIn, double muIn)
        {
            dx = dxIn;
            dy = dyIn;
            dz = dzIn;
            w = wIn;
            f = fIn;
            k = kIn;
            mu = muIn;

            transl = new Matrix(3, 1);
            vRot = new Matrix(3, 3);            
            transl.SetColumnVector(Vector.Create(new double[] { dxIn, dyIn, dzIn }), 0);

            rot = new Matrix(3, 3);
            rot.SetRowVector(Vector.Create(new double[] {    1, -kIn,  fIn }), 0);
            rot.SetRowVector(Vector.Create(new double[] {  kIn,    1, -wIn }), 1);
            rot.SetRowVector(Vector.Create(new double[] { -fIn,  wIn,    1 }), 2);
            mu = muIn;
        }

        /// <summary>
        /// Definição da Transformação de Helmert a 14 Parâmetros
        /// </summary>
        /// <param name="dxIn">translacção em x (metros)</param>
        /// <param name="dyIn">translacção em y (metros)</param>
        /// <param name="dzIn">translacção em z (metros)</param>
        /// <param name="wIn">rotação em x (rad)</param>
        /// <param name="fIn">rotação em y (rad)</param>
        /// <param name="kIn">rotação em z (rad)</param>
        /// <param name="muIn">factor de escala</param>
        /// <param name="vdxIn">variação da translacção em x (metros)</param>
        /// <param name="vdyIn">variação da translacção em y (metros)</param>
        /// <param name="vdzIn">variação da translacção em z (metros)</param>
        /// <param name="vwIn">variação da rotação em x (rad)</param>
        /// <param name="vfIn">variação da rotação em y (rad)</param>
        /// <param name="vkIn">variação da rotação em z (rad)</param>
        /// <param name="vmuIn">variação do factor de escala</param>
        /// <param name="epoca0">epoca inicial</param>
        /// <param name="epocaFinal">epoca final</param>
        public Helmert(double dxIn, double dyIn, double dzIn, double wIn, double fIn, double kIn, double muIn, double vdxIn, double vdyIn,
                       double vdzIn, double vwIn, double vfIn, double vkIn, double vmuIn, int epoca0In, int epocaFinalIn)
        {
            dx = dxIn;
            dy = dyIn;
            dz = dzIn;
            w = wIn;
            f = fIn;
            k = kIn;
            mu = muIn;
            vdx = vdxIn;
            vdy = vdyIn;
            vdz = vdzIn;
            vw = vwIn;
            vf = vfIn;
            vk = vkIn;
            vmu = vmuIn;
            epoca0 = epoca0In;
            epocaFinal = epocaFinalIn;

            transl = new Matrix(3, 1);
            vTransl = new Matrix(3, 1);
            rot = new Matrix(3, 3);
            vRot = new Matrix(3, 3);
            Vector a = new Vector(3) { dxIn, dyIn, dzIn };
            transl.SetColumnVector(a, 0);

            vTransl.SetColumnVector(Vector.Create(new double[] { vdxIn, vdyIn, vdzIn }), 0);
            
            rot.SetRowVector(Vector.Create(new double[] {    1, -kIn,  fIn }), 0);
            rot.SetRowVector(Vector.Create(new double[] {  kIn,    1, -wIn }), 1);
            rot.SetRowVector(Vector.Create(new double[] { -fIn,  wIn,    1 }), 2);

            vRot.SetRowVector(Vector.Create(new double[] {     0, -vkIn,  vfIn }), 0);
            vRot.SetRowVector(Vector.Create(new double[] {  vkIn,     0, -vwIn }), 1);
            vRot.SetRowVector(Vector.Create(new double[] { -vfIn,  vwIn,     0 }), 2);

            mu = muIn;
            vmu = vmuIn;

            t0 = epoca0;
            t = epocaFinal;
        }

        /// <summary>
        /// Aplicar a transformação 7 parâmetros
        /// </summary>
        /// <param name="coordRet">GeoCoord de coordenadas a transformar</param>
        /// <returns></returns>
        public GeoCoord helmert7(GeoCoord coordRet)
        {
            GeoCoord coordRetTmp = new GeoCoord();
            Matrix cIn = new Matrix(3, 1);
            Matrix cOut = new Matrix(3, 1);
            Height a = new Height();
            

            foreach (Ret ptoTmpI in coordRet.RetList)
            {
                Ret ptoTmpO = new Ret();
                cIn[0, 0] = ptoTmpI.X;
                cIn[1, 0] = ptoTmpI.Y;
                cIn[2, 0] = ptoTmpI.Z;

                ptoTmpO.ListDadosDivD = ptoTmpI.ListDadosDivD;
                ptoTmpO.ListDadosDivS = ptoTmpI.ListDadosDivS;
                ptoTmpO.ID = ptoTmpI.ID;

                cOut = transl + (1 + mu) * rot * cIn;

                ptoTmpO.X = cOut[0, 0];
                ptoTmpO.Y = cOut[1, 0];
                ptoTmpO.Z = cOut[2, 0];

                coordRetTmp.addRetPoint(ptoTmpO);
            }

            return coordRetTmp;
        }

        /// <summary>
        /// Aplicar a transformação a 7 parâmetros a partir dos parâmetros inversos definidos
        /// </summary>
        /// <param name="coordRet"></param>
        /// <returns></returns>
        public GeoCoord Helmert7Inverse(GeoCoord coordRet)
        {
            Helmert tmp = new Helmert(this.dx * -1, this.dy * -1, this.dz * -1, this.w * -1, this.f * -1, this.k * -1, this.mu * -1);
            return tmp.helmert7(coordRet);
        }

        /// <summary>
        /// Aplicar a transformação 14 parâmetros
        /// </summary>
        /// <param name="coordRet">GeoCoord de coordenadas a transformar</param>
        /// <returns></returns>
        public GeoCoord helmert14(GeoCoord coordRet)
        {
            GeoCoord coordRetTmp = new GeoCoord();
            Matrix cIn = new Matrix(3, 1);
            Matrix vcIn = new Matrix(3, 1);
            Matrix cOut = new Matrix(3, 1);

            foreach (Ret ptoTmpI in coordRet.RetList)
            {
                Ret ptoTmpO = new Ret();
                cIn.SetColumnVector(new Vector(3) { ptoTmpI.X, ptoTmpI.Y, ptoTmpI.Z }, 0);
                vcIn.SetColumnVector(new Vector(3) { ptoTmpI.Vel_X, ptoTmpI.Vel_Y, ptoTmpI.Vel_Z }, 0);

                cOut = transl + (1 + mu) * (rot * (cIn + vcIn * (t - t0))) +
                           (vTransl + ((1 + mu) * vRot + vmu * rot) * cIn) * (t - t0);

                ptoTmpO.ListDadosDivD = ptoTmpI.ListDadosDivD;
                ptoTmpO.ListDadosDivS = ptoTmpI.ListDadosDivS;
                ptoTmpO.ID = ptoTmpI.ID;
                                
                ptoTmpO.X = cOut[0, 0];
                ptoTmpO.Y = cOut[1, 0];
                ptoTmpO.Z = cOut[2, 0];

                coordRetTmp.addRetPoint(ptoTmpO);
            }

            return coordRetTmp;
        }

        /// <summary>
        /// Aplicar a transformação a 14 parâmetros a partir dos parâmetros inversos definidos 
        /// </summary>
        /// <param name="coordRet"></param>
        /// <returns></returns>
        public GeoCoord Helmert14Inverse(GeoCoord coordRet)
        {
            Helmert tmp = new Helmert(this.dx * -1, this.dy * -1, this.dz * -1, this.w * -1, this.f * -1, this.k * -1, this.mu * -1,
                                      this.vdx * -1, this.vdy * -1, this.vdz * -1, this.vw * -1, this.vf * -1, this.vk * -1,
                                      this.vmu * -1, this.epocaFinal, this.epoca0);
            return tmp.helmert14(coordRet);
        }
    }
}
