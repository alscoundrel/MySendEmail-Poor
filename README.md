# MySendEmail-Poor
 Sem linha de Comandos

# definições de acesso ao SendGrid e conta de origem
<b>Ficheiro appsettings.json</b><br>
{<br>
    "SendGrid": {<br>
        "apiKey": "YOU-API",<br>
        "from": "FROM-EMAIL",<br>
        "fromname": "FROM-NAME"<br>
    }<br>
}<br>
Alterar para as suas chaves e dados<br>

# dados para usar no modelo e envio
<b>Ficheiro sendvalues.json</b><br>
{<br>
    "subject":"Para si {{name}} - E-mail pelo SendGrid com Scriban",<br>
    "addresses" :[<br>
        {"email": "EMAIL-COUNT1, "name":"NAME1"},<br>
        {"email": "EMAIL-COUNT2", "name":"NAME2"}<br>
    ]<br>
}<br>
O elemento "subject" é usado como assunto para o e-mail de envio.<br>
É definido no elemento "addresses" as contas para envio (altere para as suas contas).<br>
