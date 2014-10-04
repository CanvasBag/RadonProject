using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BaseCoordinates.Elements;
using BaseCoordinates.Geometry;
using BaseCoordinates.Seed;
using MathNet.Numerics.LinearAlgebra;
using System.Xml;
using GeoMathLib;
using AjustLeastSquare;
using BaseCoordinates.Elements;
using BaseCoordinates.Seed;
using Meta.Numerics.Statistics.Distributions;

namespace GeoMathLib.Calc

public class Ellipse
{
	private EastingNorthing point;
	private double var, df;
	private double alpha;

	private double qxx, qyy, qxy;
	private double quu, qvv;
	private double t;
	private double su, sv;
	private double suC, svC;

	public Ellipse(EastingNorthing point, Matrix Qxx, double var)
	{
		this.var = var;

		SetPoint(point, Qxx);

		ComputeStandardErrorEllipse();
	}
	
	public Ellipse(EastingNorthing point, Matrix Qxx, double var, double df, double alpha)
	{
		this.var = var;
		this.df = df;
		this.alpha = alpha;

		SetPoint(point, Qxx);

		ComputeErrorEllipseConfidenceLevel();
	}

	public void SetPoint(EastingNorthing newPoint, Matrix Qxx)
	{
		point = newPoint;
		SetQxx(Qxx);
	}

	public void SetQxx(Matrix Qxx)
	{
		qxx = Qxx[point.ListDadosDivInt[0] * 2, point.ListDadosDivInt[0] * 2];
		qyy = Qxx[point.ListDadosDivInt[0] * 2 + 1, point.ListDadosDivInt[0] * 2 + 1];
		qxy = Qxx[point.ListDadosDivInt[0] * 2, point.ListDadosDivInt[0] * 2 + 1];
	}

	public void ComputeStandardErrorEllipse()
	{
		ComputeTDuDv();

		su = Math.Sqrt(var) * Math.Sqrt(quu);
		sv = Math.Sqrt(var) * Math.Sqrt(qvv);
	}

	public void ComputeErrorEllipseConfidenceLevel()
	{
		FisherDistribution F = new FisherDistribution(2, df);
		double c = Math.Sqrt(2.0 * F.InverseRightProbability(alpha));

		ComputeStandardErrorEllipse();

		suC = c * su;
		svC = c * sv;
	}

	private void ComputeTDuDv()
	{
		t = ComputeT();
		quu = ComputeQuu();
		qvv = ComputeQvv();
	}

	private double ComputeT()
	{
		double t;

		t = Math.Atan2(2.0 * qxy, qyy - qxx);

		if (t < 0)
			t += 2.0 * Math.PI;

		t /= 2.0;

		return t;
	}

	private double ComputeQuu()
	{
		// qxx * sin(t)^2 + 2 * cos(t) * sin(t) + qyy * cos(t) ^2
		return qxx * Math.Pow(Math.Sin(t), 2) + 2 * qxy * Math.Cos(t) * Math.Sin(t) + qyy * Math.Pow(Math.Cos(t), 2);
	}

	private double ComputeQvv()
	{
		// qxx * cos(t)^2 + 2 * cos(t) * sin(t) + qyy * cos(t) ^2
		return qxx * Math.Pow(Math.Cos(t), 2) - 2 * qxy * Math.Cos(t) * Math.Sin(t) + qyy * Math.Pow(Math.Sin(t), 2);
	}

	public double Alpha
	{
		get { return alpha; }
		set { alpha = value; }
	}

	public double Var
	{
		get { return var; }
		set { var = value; }
	}

	public double Df
	{
		get { return df; }
		set { df = value; }
	}

	public double T
	{
		get { return t; }
		set { t = value; }
	}

	public double Su
	{
		get { return su; }
		set { su = value; }
	}

	public double Sv
	{
		get { return sv; }
		set { sv = value; }
	}

	public double SuC
	{
		get { return suC; }
		set { suC = value; }
	}

	public double SvC
	{
		get { return svC; }
		set { svC = value; }
	}
}