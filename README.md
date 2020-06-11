# chatter

## Considerações
O uso do mediator possibilitou deixar o código bem desacoplado, permitindo inclusive a utilização de plugins. No entando, trouxe um nível de complexidade que acredito não ser a melhor decisão arquitetural para um projeto deste tamanho, além de um maior custo de perfomance. É apenas um exemplo de uma solução que permite bastante flexibilidade.

Não foi utilizado TDD. TDD tem muitas vantagens, mas possui desvantagens também. Uma deles é um maior tempo no ciclo do desenvolvimento. Devido ao curto prazo de entrega, existia a possibilidade de não ser entregue todo os itens requisitados, então os testes foram criados após o desenvolvimento.

Execute o arquivo styker.bat para verificar a qualidade dos testes, utilizando o _teste de mutação_.
