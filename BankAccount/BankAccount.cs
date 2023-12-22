using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts
{
    public class BankAccount
    {
        public static BankAccount[] _accounts = new BankAccount[3];
        private int account_number;
        private string account_name;
        private float amount;
        public BankAccount(int a, string b, float c)
        {
            account_number = a;
            account_name = b;
            if (c >= 0) amount = c;
            else
            {
            new_c:
                Console.WriteLine("Значение не может быть отрицательным, введите значение больше нуля:\n");
                c = float.Parse(Console.ReadLine());
                if (c < 0) goto new_c;
            }
        }
        // Демонстрация методов добавления, уменьшения и обнуленияя счетов
        public void actions()
        {
        choose:
            if (amount == 0)
            {
            again:
                Console.Clear();
                Console.WriteLine("На счету закончились деньги. Выберите другой аккаунт\n");
                for (int i = 0; i < _accounts.Length; i++)
                {
                    _accounts[i].cout();
                }
                int a = int.Parse(Console.ReadLine());
                if (a <= 3)
                {
                    Console.Clear();
                    _accounts[a - 1].cout();
                    _accounts[a - 1].actions();
                }
                else
                {
                    Console.WriteLine("Такого аккаунта не существует\n");
                    goto again;
                }
            }
            Console.WriteLine("\nДобавление: 1\n");
            Console.WriteLine("Убавление: 2\n");
            Console.WriteLine("Обнуление: 3\n");
            Console.WriteLine("Перевод на другой аккаунт: 4\n");
            Console.WriteLine("Выберите действие:");
            int choose = int.Parse(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    Console.WriteLine($"\nВведите, сколько вы хотите добавить счёту:");
                    dob(float.Parse(Console.ReadLine()));
                    Console.Clear();
                    cout();
                    goto choose;
                case 2:
                    Console.WriteLine($"\nВведите, сколько вы хотите уменьшить счёту:");
                    umen(float.Parse(Console.ReadLine()));
                    Console.Clear();
                    cout();
                    goto choose;
                case 3:
                    Console.WriteLine("\nСейчас будет произведено обнуление");
                    obnul();
                    Console.Clear();
                    cout();
                    goto choose;
                case 4:
                    for (int i = 0; i < _accounts.Length; i++)
                    {
                        _accounts[i].cout();
                    }
                    Console.WriteLine("\nВведите номер аккаунта, с которого стоит списать средства, и номер аккаунта, на который стоит их зачислить");
                    transition(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
                    goto choose;
            }
        }
        // Вывод данных об аккаунте
        public void cout()
        {
            Console.WriteLine($"\nНомер счета: {account_number}\n");
            Console.WriteLine($"ФИО владельца счета: {account_name}\n");
            Console.WriteLine($"Сумма на счету: {amount}\n");
        }
        // Метод добавления
        private void dob(float sum)
        {
            if (sum >= 0) amount += sum;
            else
            {
            new_sum1:
                Console.WriteLine("\nЗначение не может быть отрицательным, введите другое значение:");
                sum = float.Parse(Console.ReadLine());
                if (sum < 0) goto new_sum1;
                else amount += sum;
            }
        }
        // Метод уменьшения
        private float umen(float sum)
        {
        check:
            if (sum > 0 && sum < amount || sum == amount) amount -= sum;
            else if (sum < 0)
            {
            new_sum2:
                Console.WriteLine("Значение не может быть отрицательным, введите другое значение:");
                sum = float.Parse(Console.ReadLine());
                if (sum <= amount) amount -= sum;
                else if (sum < 0) goto new_sum2;
            }
            else if (sum > amount)
            {
                Console.WriteLine("На счету нет столько денег для списания, введите сумму поменьше.");
                sum = float.Parse(Console.ReadLine());
                goto check;
            }
            return sum;
        }
        // Метод обнуления
        private void obnul()
        {
            amount = 0;
        }
        // Метод перевода средств с одного аккаунта на другой
        public static void transition(int a, int b)
        {
        again:
            if (a <= 3) Console.WriteLine("Введите сумму, которую необходимо списать:");
            else
            {
                Console.WriteLine("Такого аккаунта не существует\n");
                goto again;
            }
            if (b == a)
            {
                Console.WriteLine("Пожалуйста, выберете другой аккаунт для перевода\n");
                goto again;
            }
            if (b > 3)
            {
                Console.WriteLine("Такого аккаунта не существует\n");
                goto again;
            }
            _accounts[b - 1].operation_dob(_accounts[a - 1].operation_umen());
            Console.Clear();
            for (int i = 0; i < _accounts.Length; i++)
            {
                _accounts[i].cout();
            }
        }
        // Метод уменьшения для перевода
        public float operation_umen()
        {
            return umen(float.Parse(Console.ReadLine()));
        }
        // Метод добавления для перевода
        public void operation_dob(float c)
        {
            if (c >= 0) amount += c;
            cout();
        }
    }
}