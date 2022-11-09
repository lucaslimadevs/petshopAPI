# Banco mySQL
1 - Instalar o workbench => https://dev.mysql.com/downloads/workbench/
2 - Instalar o wampserver para rodar o banco. Link => https://sourceforge.net/projects/wampserver/files/
3 - Abrir o wampserver e verificar se esta com icone verde na barra de tarefas.
4 - Com o wampserver rodando, abrir o workbench e clicar na instancia local.
5 - Criar Schema nomeado "petshop" clicando no "navigator" com botão direito.
6 - No workbench ir na opção (server => Data import)
7 - Selecionar a opção "import from Self-Contained File" e selecionar o script "petshop_script.sql" que se encontra 
    na pasta "Script" dentro do repositorio "petshop_API"
8 - selecionar no "Default Target Schema" o schema "petshop"
9 - Na aba "Import Progress" clicar em "Start Import" aguardar e o banco e dados foi importado!
 
configuração do banco:
  Server=localhost;
  Database=petshop;
  Uid=root;
  Pwd=;

# petshop_API 
1 - Rodar a aplicação no Visual Studio 2019
2 - Na pasta "Collection_postman" existe uma Collection com as requisições utilizadas na API
    que podem ser importadas no "postaman" para teste.