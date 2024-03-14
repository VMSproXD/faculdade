class Main:
    pass

print("Testando o projeto")

from Cliente import Cliente

c1 = Cliente ("Vinicius","11-4937-6343")

from Conta import Conta

c1=Cliente ("Vinicius","11-94937-6343")
Conta=Conta(c1.__init__(),6565)

Conta.deposita(100)
Conta.saque(50)
Conta.extrato()