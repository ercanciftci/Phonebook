# Phonebook
Proje için ContactAPI (.NET 6.0) ve ReportAPI (.NET 6.0) mikro servisleri oluşturuldu.
MongoDB.Driver kullanıldı.
Her iki mikro servis için ayrı db ler oluşturuldu.
Proje vs code üzerinden docker-compose up komutu ile ayağa kaldırılır. İki mikro servis için Image oluşturuldu.
Message Queue olarak RabbitMQ kullanıldı.
ReportAPI Create metodu ile Report oluşturulduğunda kuyruğa mesaj gönderilir. Bu mesajı dinleyen Consumer ContactAPI nin GetLocationDataList metoduna Rest request
yaparak datayı alır ve Excel dosyasını oluşturur. Oluşan Excel dosyaları ReportAPI reports klasöründe saklanır. 
Örneğin: http://localhost:5012/reports/ae6f2f02-a420-4ff8-8410-d3b7af649062.xlsx  Dosya ismi Report tablosunda ReportFileName alanına set edilir.
Report kaydı güncellenir.
