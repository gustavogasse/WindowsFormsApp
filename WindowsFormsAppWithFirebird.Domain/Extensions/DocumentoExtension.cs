using System;

namespace WindowsFormsAppWithFirebird.Domain.Extensions
{
    public static class DocumentoExtension
    {
        public static bool EhValido(this string value)
        {
            return (value.EhCpf() || value.EhCnpj());
        }

        private static bool EhCpf(this string value)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            value = value.Trim().RemoverCaracteres();
            if (value.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == value)
                    return false;

            string tempCpf = value.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return value.EndsWith(digito);
        }

        private static bool EhCnpj(this string value)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            value = value.Trim().RemoverCaracteres();
            if (value.Length != 14)
                return false;

            string tempCnpj = value.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return value.EndsWith(digito);
        }

        public static string RemoverCaracteres(this string value)
        {
            return value
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace("-", string.Empty)
                .Replace("/", string.Empty);
        }

        public static string FormatarCNPJ(this string value)
        {
            return value.EhCnpj()
                ? Convert.ToUInt64(value.RemoverCaracteres()).ToString(@"00\.000\.000\/0000\-00")
                : string.Empty;
        }

        public static string FormatarCPF(this string value)
        {
            return value.EhCpf()
                ? Convert.ToUInt64(value.RemoverCaracteres()).ToString(@"000\.000\.000\-00")
                : string.Empty;
        }
    }
}
