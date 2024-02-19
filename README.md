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
- Post  /api/users/register :
- Post  /api/users/login :
- Get   /api/users/get-all-users :
- Put   /api/users/role-assign :

## (Accounts Controller)
- Post  /api/accounts/create-account :
- Get   /api/accounts/{accountId}/balance :
- Put   /api/accounts/{accountId}/deposit-balance :
- Put   /api/accounts/{accountId}/withdraw-balance :
