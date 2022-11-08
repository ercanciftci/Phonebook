# Phonebook
Proje için ContactAPI (.NET 6.0) ve ReportAPI (.NET 6.0) mikro servisleri oluşturuldu.
MongoDB.Driver kullanıldı.
Her iki mikro servis için ayrı db ler oluşturuldu.
Proje vs code üzerinden docker-compose up komutu ile ayağa kaldırılır. İki mikro servis için Image oluşturuldu.
Message Queue olarak RabbitMQ kullanıldı.
ReportAPI Create metodu ile Report oluşturulduğunda kuyruğa mesaj gönderilir. Bu mesajı dinleyen Consumer ContactAPI nin GetLocationDataList metoduna Rest request
yaparak datayı alır ve Excel dosyasını oluşturur. Oluşan Excel dosyaları ReportAPI reports klasöründe saklanır. Report kaydı güncellenir.
