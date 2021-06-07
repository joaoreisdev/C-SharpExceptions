// using _05_ByteBank;

using System;

namespace ByteBank
{
    public class ContaCorrente
    {

        public ContaCorrente(int agencia, int numero)
        {
            if (agencia <= 0)
            {
                ArgumentException e = new ArgumentException("O argumento agencia deve ser maior que zero", nameof(agencia));
                // Queremos lançar uma exceção para agência menor que zero
                throw e;
            }
            if (agencia <= 0)
            {
                ArgumentException e = new ArgumentException("O argumento número deve ser maior que zero", nameof(numero));
                // Queremos lançar uma exceção para número menor que zero
                throw e;
            }

            Agencia = agencia;
            Numero = numero;


            TotalDeContasCriadas++;
            TaxaOperacao = 30 / TotalDeContasCriadas;
        }

        public Cliente Titular { get; set; }

        public static int TotalDeContasCriadas { get; private set; }

        public int Numero { get; }

        private double _saldo = 100;

        public static int TaxaOperacao { get; private set; }

        public int Agencia { get; }

        public double Saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                _saldo = value;
            }
        }

        public int ContadorSaquesNaoPermitidos { get; private set; }
        public int ContadorTransferenciasNaoPermitidos { get; private set; }

        public void Sacar(double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para o saque.", nameof(valor));
            }

            if (_saldo < valor)
            {
                ContadorSaquesNaoPermitidos++;

                throw new SaldoInsuficienteException(_saldo, valor);
            }

            _saldo -= valor;
            
        }

        public void Depositar(double valor)
        {
            _saldo += valor;
        }


        public void Transferir(double valor, ContaCorrente contaDestino)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para a transferencia.", nameof(valor));
            }


            try
            {
                // Chama o método sacar para realizar a retirada do que será transferido
                Sacar(valor);
            }
            catch (SaldoInsuficienteException e)
            {
                ContadorTransferenciasNaoPermitidos++;
                throw new OperacaoFinanceiraException("Operação não realizada.", e);
            }

            contaDestino.Depositar(valor);
            
        }
    }
}
