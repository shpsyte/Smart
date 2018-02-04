using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Fake
{
    public enum TaxFunctionEnum
    {
        Vendas = 1,
        Devolucao = 2,
        Retorno = 3,
        Transferencia = 4,
        Complemento = 5
    }


    public enum TaxWayEnum
    {
        Entrada = 1, Saida = 2
    }

    public enum FinancialTypeEnum
    {
        [Display(Name = "Contas a Pagar")]
        Pagar = 1,
        [Display(Name = "Contas a Receber")]
        Receber = 2


    }

    public enum PaymentUseEnum
    {
        [Display(Name = "Todos")]
        Todos = 1,
        [Display(Name = "Vendas")]
        Vendas = 2,
        [Display(Name = "Compras")]
        Compras = 2

    }


    public enum ClassPersonEnum
    {
        [Display(Name = "Pessoa Jurídica")]
        Juridica = 1,
        [Display(Name = "Pessoa Física")]
        Fisica  = 2

    }


    public enum TypePersonEnum
    {
        [Display(Name = "Contato")]
        Contato = 1,
        [Display(Name = "Cliente")]
        Cliente = 2,
        [Display(Name = "Vendedor")]
        Vendedor = 3,
        [Display(Name = "Empregado")]
        Empregado = 4,
        [Display(Name = "Fornecedor")]
        Fornecedor = 5,
        [Display(Name = "Geral/Ambos")]
        Geral = 6

    }


    public enum DebitAndCredit
    {
        [Display(Name = "Crédito")]
        Credito = 1,
        [Display(Name = "Débito")]
        Debito = 2

    }


    public enum ProductSource
    {
        [Display(Name = "Nacional")]
        Nacional = 0,
        [Display(Name = "Importado")]
        Importado = 1,
        [Display(Name = "Importado, adquirido mercado interno")]
        CompradoMercadoInterno = 2

    }


}
