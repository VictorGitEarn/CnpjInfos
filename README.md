# Projeto: CnpjInfos

## Sobre
Projeto para teste prático da Fº360.

## Configuração do projeto
- Ter instalado o MongoDb;
- Ter o WebDriver do chrome disponível em uma pasta do C//, recomendável C:\\WebDriver\\;
- Configurar os AppSettings de cada projeto:

CI.DownloadFiles.Worker: (Se atentar com o path da pasta de download "DownloadDir")
{
  "Provider": "ReceitaFederal",
  "WebDrivers": "C:\\WebDriver\\",
  "ReceitaFederalUrl": "https://receita.economia.gov.br/orientacao/tributaria/cadastros/cadastro-nacional-de-pessoas-juridicas-cnpj/dados-publicos-cnpj",
  "ShouldDownloadFromDemo": "true",
  "DemoUrl": "https://drive.google.com/file/d/11JEE8WKSD9_FBAfGfiFq_z-ZtS1bmGeR/view",
  "DemoElementToDownload": "/html/body/div[3]/div[2]/div[4]/div[4]/div[1]/div[5]/div[1]/div[2]",
  "DemoConfirmElementToDownload": "/html/body/div[2]/div[2]/a",
  "DownloadDir": "C:\\Users\\victo\\Downloads\\"
}

CI.ProcessFiles.Worker: (Se atentar com o path da pasta de download "DownloadDir" e a ConnectionString do MongoDB)
{
  "Provider": "ReceitaFederal",
  "ConnectionString": "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false",
  "DataBase": "CnpjInfos",
  "CompaniesCollection": "Companies",
  "ExtractAgain": "false",
  "DownloadDir": "C:\\Users\\USER\\Downloads\\"
}

CI.API.Rest: Se atentar para o ambiente que está sendo executado o projeto.
{
  "ConnectionString": "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false",
  "DataBase": "CnpjInfos",
  "CompaniesCollection": "Companies"
}

## Dependências
* [MongoDb](https://www.mongodb.com/)

## REFERÊNCIAS
- [MongoDb](https://www.mongodb.com/)
- [Selenium](https://www.selenium.dev/)
