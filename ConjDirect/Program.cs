using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConjDirect
{
        public delegate double MyFxDelegate(int nNumVars, ref double[] fX, ref double[] fParam);
        class Program
        {
            static void Main(string[] args)
            {
                int nNumVars = 2;
                double[] fX = new double[] { 0, 0 };
                double[] fParam = new double[] { 0, 0 };
                int nIter = 0;
                int nMaxIter = 100;
                double fEpsFx = 0.0000001;
                int i;
                double fBestF;
                string sErrorMsg = "";
                CConjugateGradient1 oOpt;
                MyFxDelegate MyFx = new MyFxDelegate(Fx3);
                oOpt = new CConjugateGradient1();

                for (i = 0; i < nNumVars; i++)
                {
                    Console.WriteLine("X({0}) = {1}", i + 1, fX[i]);
                }
                Console.WriteLine("Function tolerance = {0}", fEpsFx);
                Console.WriteLine("Maxumum cycles = {0}", nMaxIter);

                Console.WriteLine("******** FINAL RESULTS *************");
                fBestF = oOpt.CalcOptim(nNumVars, ref fX, ref fParam, fEpsFx, nMaxIter, ref nIter, ref sErrorMsg, MyFx);

                Console.WriteLine("Optimum at");
                for (i = 0; i < nNumVars; i++)
                {
                    Console.WriteLine("X({0}) = {1}", i + 1, fX[i]);
                }
                Console.WriteLine("Function value = {0}", fBestF);
                Console.WriteLine("Number of iterations = {0}", nIter);
                Console.WriteLine();
                Console.Write("Press Enter to end the program ...");
                Console.ReadLine();
            }
            static public double Fx3(int N, ref double[] X, ref double[] fParam)
            {
                return 2 * X[0] * X[0] + 2 * X[1] * X[1] + 2 * X[0] * X[1] + 20 * X[0] + 10 * X[1] + 10;
            }
            public class CConjugateGradient1
            {
                MyFxDelegate m_MyFx;
                public double MyFxEx(int nNumVars, ref double[] fX, ref double[] fParam, ref double[] fDeltaX, double fLambda)
                {
                    int i;
                    double[] fXX = new double[nNumVars];

                    for (i = 0; i < nNumVars; i++)
                    {
                        fXX[i] = fX[i] + fLambda * fDeltaX[i];
                    }

                    return m_MyFx(nNumVars, ref fXX, ref fParam);
                }

                private void GetGradients(int nNumVars, ref double[] fX, ref double[] fParam, ref double[] fDeriv, ref double fDerivNorm)
                {
                    int i;
                    double fXX, H, Fp, Fm;
                    fDerivNorm = 0;
                    for (i = 0; i < nNumVars; i++)
                    {
                        fXX = fX[i];
                        H = 0.01 * (1 + Math.Abs(fXX));
                        fX[i] = fXX + H;
                        Fp = m_MyFx(nNumVars, ref fX, ref fParam);
                        fX[i] = fXX - H;
                        Fm = m_MyFx(nNumVars, ref fX, ref fParam);
                        fX[i] = fXX;
                        fDeriv[i] = (Fp - Fm) / 2 / H;
                        fDerivNorm += Math.Pow(fDeriv[i], 2);
                    }
                    fDerivNorm = Math.Sqrt(fDerivNorm);
                }

                public bool LinSearch_DirectSearch(int nNumVars, ref double[] fX, ref double[] fParam, ref double fLambda, ref double[] fDeltaX, double InitStep, double MinStep)
                {
                    double F1, F2;

                    F1 = MyFxEx(nNumVars, ref fX, ref fParam, ref fDeltaX, fLambda);

                    do
                    {
                        F2 = MyFxEx(nNumVars, ref fX, ref fParam, ref fDeltaX, fLambda + InitStep);
                        if (F2 < F1)
                        {
                            F1 = F2;
                            fLambda += InitStep;
                        }
                        else
                        {
                            F2 = MyFxEx(nNumVars, ref fX, ref fParam, ref fDeltaX, fLambda - InitStep);
                            if (F2 < F1)
                            {
                                F1 = F2;
                                fLambda -= InitStep;
                            }
                            else
                            {
                                // reduce search step size
                                InitStep /= 10;
                            }
                        }
                    } while (!(InitStep < MinStep));

                    return true;

                }


                public double CalcOptim(int nNumVars, ref double[] fX, ref double[] fParam, double fEpsFx, int nMaxIter, ref int nIter, ref string sErrorMsg, MyFxDelegate MyFx)
                {

                    int i;
                    double[] fDeriv = new double[nNumVars];
                    double[] fDerivOld = new double[nNumVars];
                    double F, fDFNormOld, fLambda, fLastF, fDFNorm = 0;

                    m_MyFx = MyFx;

                    // calculate and function value at initial point
                    fLastF = MyFx(nNumVars, ref fX, ref fParam);

                    GetGradients(nNumVars, ref fX, ref fParam, ref fDeriv, ref fDFNorm);

                    fLambda = 0.1;
                    if (LinSearch_DirectSearch(nNumVars, ref fX, ref fParam, ref fLambda, ref fDeriv, 0.1, 0.000001))
                    {
                        for (i = 0; i < nNumVars; i++)
                        {
                            fX[i] += fLambda * fDeriv[i];
                        }
                    }
                    else
                    {
                        sErrorMsg = "Failed linear search";
                        return fLastF;
                    }

                    nIter = 1;
                    do
                    {
                        nIter++;
                        if (nIter > nMaxIter)
                        {
                            sErrorMsg = "Reached maximum iterations limit";
                            break;
                        }
                        fDFNormOld = fDFNorm;
                        for (i = 0; i < nNumVars; i++)
                        {
                            fDerivOld[i] = fDeriv[i]; // save old gradient
                        }
                        GetGradients(nNumVars, ref fX, ref fParam, ref fDeriv, ref fDFNorm);
                        for (i = 0; i < nNumVars; i++)
                        {
                            fDeriv[i] = Math.Pow((fDFNorm / fDFNormOld), 2) * fDerivOld[i] - fDeriv[i];
                        }
                        if (fDFNorm <= fEpsFx)
                        {
                            sErrorMsg = "Gradient norm meets convergence criteria";
                            break;
                        }
                        fLambda = 0;
                        if (LinSearch_DirectSearch(nNumVars, ref fX, ref fParam, ref fLambda, ref fDeriv, 0.1, 0.000001))
                        {
                            for (i = 0; i < nNumVars; i++)
                            {
                                fX[i] += fLambda * fDeriv[i];
                            }
                            F = MyFx(nNumVars, ref fX, ref fParam);
                            if (Math.Abs(F - fLastF) < fEpsFx)
                            {
                                sErrorMsg = "Successive function values meet convergence criteria";
                                break;
                            }
                            else
                            {
                                fLastF = F;
                            }
                        }
                        else
                        {
                            sErrorMsg = "Failed linear search";
                            break;
                        }
                    } while (true);

                    return fLastF;
                }
            }
        }
    
}
