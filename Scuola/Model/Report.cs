using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Model {
    public class Report {
        public int NumEdition { get; set; }
        public decimal SumPrices { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MedPrice { get; set; }
        public decimal ModaPrice { get; set; }
        public int MaxStudents { get; set; }
        public int MinStudents { get; set; }
        public Report(int numEdition, decimal sumPrices, decimal avgPrice, decimal medPrice, decimal modaPrice, int maxStudents, int minStudets){
            NumEdition = numEdition;
            SumPrices = sumPrices;
            AveragePrice = avgPrice;
            MedPrice = medPrice;
            ModaPrice = ModaPrice;
            MaxStudents = maxStudents;
            MinStudents = minStudets;
        }
        public Report(){
        }
        public override string ToString()
        {
            return $" - Numero Edizione : {NumEdition} \n " +
                $" - Somma dei prezzi : {SumPrices} \n " +
                $" - Prezzo Medio : {AveragePrice} \n " +
                $" - Mediana Prezzo : {MedPrice} \n " +
                $" - Moda Prezzo : {ModaPrice} \n " +
                $" - Numero studenti massimi : {MaxStudents} \n " +
                $" - Numero studenti minimi : {MinStudents} \n ";
        }
    }
}
