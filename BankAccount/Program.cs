using BankAccounts;
//Создание аккаунтов
for (int i = 0; i < 3; i++)
{
    Console.WriteLine($"Введите ФИО и сумму аккаунта №{i + 1}");
    BankAccount._accounts[i] = new BankAccount(i + 1, Console.ReadLine(), float.Parse(Console.ReadLine()));
}
// Добавление & уменьшение счета
again:
Console.WriteLine("Введите, над каким аккаунтом произвести действие:");
int a = int.Parse(Console.ReadLine());
Console.Clear();
BankAccount._accounts[a - 1].cout();
if (a <= 3) BankAccount._accounts[a - 1].actions();
else
{
    Console.WriteLine("Такого аккаунта не существует\n");
    goto again;
}