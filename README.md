# .NET 7 ile Geliştirilmiş Bankacılık API'si (Fimple .Net Final Case)

## Giriş
Günümüzde, bankacılık hizmetleri giderek daha fazla dijitalleşmekte ve müşteri beklentileri sürekli olarak evrilmektedir. Bu değişim, bankacılık sistemlerinin güvenli, etkili ve kullanıcı dostu web servisleriyle donatılmasını zorunlu kılmaktadır. Bu proje .NET platformu üzerinde RESTful API'ler geliştirmeyi amaçlamaktadır. 

## Özellikler
- Kullanıcı kaydı ve yönetimi.
- Hesap işlemleri, bakiye sorgulama ve güncelleme.
- Para transferleri ve işlem kayıtları.
- Kredi başvurusu, ödeme planları ve sorgulama.
- Otomatik ödeme ayarları ve yönetimi.
- Müşteri destek talepleri ve takibi.

## Teknolojiler
- .Net Core
- SQL Server
- JWT ile kimlik doğrulama
- Entity Framework

## API Detayları ve Dökümantasyonu

## (Users Controller)
- Post  /api/users/register : Creates a new user (Admin only).
- Get   /api/users/get-all-users : Displays all users (Admin only).
- Put   /api/users/role-assign : Makes role assignment (Admin only).

## (Accounts Controller)
- Post  /api/accounts/create-account : Creates a new account (Admin only).
- Get   /api/accounts/{accountId}/balance : Queries the balance of a specific account.
- Put   /api(accounts/id/update-balance : Updates the balance of a specific account (Admin only).

## (Login Controller)
- Post   /api/Login/login : User logs in and returns JWT token.

## (Transactions Controller)
- Post   /api/transactions/withdraw : Withdrawing money from the account.
- Post   /api/transactions/deposit : Deposit money into the account.
- Post   /api/transactions/transfer/internal : Internal transfer between accounts.
- Post   /api/transactions/transfer/external : External transfer between accounts.
