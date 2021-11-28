using System;
using System.Globalization;

namespace Huka_Djivisa
{
    class Huka_Djivisa
    {
        public double[] variables;        //переменные
        public double epselon = 0.1;      //точность
        public double alpha = 2;          //коэффициен уменьшения
        public double step = 1;           //приращение

        private double[] _multipliers;    
        private double[] _devidends;     
        private int _numberOfVarialbles;


        public Huka_Djivisa(int numberOfVariables, double[] multipliers,  double[] devidends)
        {
            this.variables = new double[numberOfVariables];
            for(int i = 0; i < numberOfVariables; i++ )
            {
                this.variables[i] = 1;
            }
            this._numberOfVarialbles = numberOfVariables;
            this._devidends = devidends;
            this._multipliers = multipliers;
        }

        // Минимизация функции
        public void MinimazeFunction()
        {
            while (true)
            {
                double baseResult = FunctionResult(this.variables);

                double[] newPoint = CalculateNewBasePoint();
                double baseNewResult = FunctionResult(newPoint);
                PrintVector();

                bool circule = true;
                while (circule)
                {

                    this.variables = FindNewPoint(newPoint);
                    PrintVector();
                    baseResult = FunctionResult(this.variables);
                    
                    if(CompareArray(CalculateNewBasePoint(), newPoint))
                    {
                        this.variables = newPoint;
                        circule = false;
                    }                 

                    newPoint = CalculateNewBasePoint();
                }
                if (this.step < this.epselon)
                    return;
                step /= this.alpha;

            }

        }

        // Сравнение 2 массивов
        public bool CompareArray(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for(int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }

        //Спуск в направлении вектора
        public double[] FindNewPoint(double[] vectorX)
        {
            double[] resultVector = new double[this._numberOfVarialbles];

            for(int i = 0; i < resultVector.Length; i++)
            {
                resultVector[i] = this.variables[i] + 2 * (vectorX[i] - this.variables[i]);
            }
            return resultVector;
        }
        
        // Вычисление значения функции в точке
        public double FunctionResult(double[] variables)
        {
            double result = 0;
            for(int i = 0; i < variables.Length; i++)
            {
                result += this._devidends[i] / variables[i] + this._multipliers[i] * variables[i];
            }
            return result;
        }

        // Поиск новой точке сдвигом 
        public double[] CalculateNewBasePoint()
        {
            double[] newBasePoint = new double[this._numberOfVarialbles];
            for(int i = 0; i < this._numberOfVarialbles; i++)
            {
                newBasePoint[i] = this.variables[i];
            }

            for (int i = 0; i < this._numberOfVarialbles; i++)
            {
                // Вычисление значение функции в базовой точке
                double baseResult = FunctionResult(newBasePoint); 

                // Вычисление значения функции в базовой точке с увеличенной координатой
                newBasePoint[i] = newBasePoint[i] + step;
                double baseResultInc = FunctionResult(newBasePoint);

                if (baseResultInc < baseResult)
                {
                    continue;
                }

                // Вычисление значения функции в базовой точке с уменьшенной координатой
                newBasePoint[i] = newBasePoint[i] - 2 * step;
                double baseResultDec = FunctionResult(newBasePoint);

                if (baseResultDec < baseResult)
                {
                    continue;
                }
                newBasePoint[i] = newBasePoint[i] + step;
            }
            return newBasePoint;
        }

        // Вывод значений
        public void PrintVector()
        {
            string specifier = "F";
            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");
            
            Console.Write($"{FunctionResult(this.variables).ToString(specifier, culture)}:  ");

            foreach(var item in this.variables)
            {
                Console.Write($" {item.ToString(specifier, culture)} ");
            }
            Console.WriteLine();
        }

    }
}
