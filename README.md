# MySendEmail-Poor
 Sem linha de Comandos

# definições de acesso ao SendGrid e conta de origem
Ficheiro appsettings.json
{
    "SendGrid": {
        "apiKey": "YOU-API",
        "from": "FROM-EMAIL",
        "fromname": "FROM-NAME"
    }
}
Alterar para as suas chaves e dados

# dados para usar no modelo e envio
Ficheiro sendvalues.json
{
    "subject":"Para si {{name}} - E-mail pelo SendGrid com Scriban",
    "addresses" :[
        {"email": "EMAIL-COUNT1, "name":"NAME1"},
        {"email": "EMAIL-COUNT2", "name":"NAME2"}
    ]
}
O elemento "subject" é usado como assunto para o e-mail de envio.
É definido no elemento "addresses" as contas para envio (altere para as suas contas).
