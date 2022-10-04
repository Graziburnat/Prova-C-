using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class FolhaPagamento
    {
        public FolhaPagamento() => CriadoEm = DateTime.Now;

        [ForeignKey("funcionario")]
        public int idFuncionario { get; set; }
        public Funcionario funcionario { get; set; }

        [Key]
        public int Id { get; set; }

        public double valorHora { get; set; }

        public int quantHoras { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CriadoEm { get; set; }

        public double salBruto => calSB();

        public double impRenda => calIR();

        public double impINSS => calINSS();

        public double impFGTS => calFGTS();

        public double salLiq => calSL();

        private double calSB()
        {
            return ((valorHora * quantHoras));
        }

        private double calIR()
        {

            if (salBruto <= 1903.98)
            {
                return ((0));

            }
            else if (salBruto <= 2826.65)
            {
                return ((142.80));

            }
            else if (salBruto <= 3751.05)
            {
                return ((354.80));

            }
            else if (salBruto <= 4664.68)
            {
                return ((636.13));

            }
            else
            {
                return ((869.36));
            }

        }

        private double calINSS()
        {
            if (salBruto <= 1693.72)
            {
                return ((salBruto * 0.08));

            }
            else if (salBruto <= 2882.90)
            {
                return ((salBruto * 0.09));

            }
            else if (salBruto <= 5645.80)
            {
                return ((salBruto * 0.11));

            }
            else
            {
                return ((621.03));
            }

        }

        private double calFGTS()
        {
            return ((salBruto * 0.08));
        }

        private double calSL()
        {
            return ((salBruto - impRenda - impINSS));
        }

    }
}