# Hey Chefe!

# Problemática

O **Restaurante da Josefina** utiliza um sistema de comandas manuais para o registro dos pedidos dos clientes. Embora esse método seja simples e de baixo custo, ele apresenta diversas limitações que impactam diretamente a eficiência operacional e a qualidade do atendimento.

Entre os principais problemas identificados está a perda de comandas físicas, ocasionando dificuldades na conferência dos pedidos e possíveis prejuízos financeiros. Além disso, por serem preenchidas manualmente, as comandas estão sujeitas a erros de anotação, como itens registrados incorretamente ou informações incompletas, comprometendo a execução dos pedidos.

Outro fator relevante é a ausência de rastreabilidade. Atualmente, não existe um mecanismo que permita identificar qual colaborador realizou o registro de determinado pedido, dificultando a apuração de erros, a responsabilização e a implementação de melhorias nos processos internos.

A cozinha também enfrenta dificuldades devido à falta de padronização das comandas. A escrita manual frequentemente apresenta problemas de legibilidade, gerando interpretações equivocadas dos pedidos, retrabalho, atrasos na preparação e, em alguns casos, a entrega de pratos incorretos aos clientes.

Além disso, o processo de acompanhamento do preparo dos pedidos é totalmente manual. Os garçons precisam interromper as atividades da cozinha para verificar o status dos pedidos, uma vez que não há um sistema que informe se um pedido ainda não foi iniciado, está em preparo ou já foi concluído. Essa falta de comunicação aumenta o tempo de atendimento, reduz a produtividade da equipe e pode comprometer a experiência do cliente.

Diante desse cenário, evidencia-se a necessidade de uma solução informatizada capaz de padronizar o registro das comandas, garantir a rastreabilidade das operações, melhorar a comunicação entre salão e cozinha e proporcionar maior controle sobre o fluxo dos pedidos, reduzindo erros e aumentando a eficiência do restaurante.

# Proposta de Solução

Com base nos problemas identificados, propõe-se o desenvolvimento de um sistema informatizado de gerenciamento de comandas, com o objetivo de substituir o processo manual de registro dos pedidos por um processo digital, padronizado e rastreável.

O sistema possuirá controle de acesso por meio de autenticação de usuários. Haverá dois perfis de utilização:

- **Administrador:** responsável pelo gerenciamento do sistema, cadastro de usuários, categorias, itens do cardápio e mesas.
- **Garçom:** responsável pelo registro e acompanhamento dos pedidos realizados pelos clientes.

Cada pedido ficará vinculado ao usuário que o cadastrou, garantindo rastreabilidade durante todo o processo.

O sistema permitirá o gerenciamento dos principais elementos envolvidos na operação do restaurante: usuários, categorias, itens do cardápio, mesas e pedidos.

## Gerenciamento de Usuários

O sistema permitirá que administradores cadastrem usuários responsáveis pela utilização do sistema.

Cada usuário possuirá:

- Nome completo;
- Login;
- Senha;
- Perfil de acesso (Administrador ou Garçom);
- Status (Ativo ou Inativo).

## Cadastro de Categorias

Permitirá o cadastro das categorias dos produtos comercializados pelo restaurante, como bebidas, porções, pratos principais, sobremesas, entre outras.

Cada categoria possuirá:

- Nome;
- Status.

## Cadastro de Itens

Permitirá o cadastro dos produtos comercializados pelo restaurante.

Cada item possuirá:

- Código único;
- Nome;
- Descrição;
- Categoria;
- Preço de venda;
- Margem de lucro;
- Status.

## Cadastro de Mesas

Permitirá o gerenciamento das mesas disponíveis no restaurante.

Cada mesa possuirá:

- Código único;
- Situação:
    - Livre;
    - Ocupada;
    - Em limpeza.

## Cadastro de Pedidos

O sistema permitirá registrar pedidos realizados pelos clientes.

Cada mesa poderá possuir um ou mais pedidos, e cada pedido poderá conter um ou mais itens do cardápio.

Todo pedido será vinculado automaticamente ao usuário (garçom) responsável pelo seu cadastro.

Os pedidos possuirão os seguintes estados:

- Não iniciado;
- Em andamento;
- Concluído (aguardando entrega);
- Concluído e entregue;
- Cancelado.

Esses estados permitirão que garçons acompanhem em tempo real o andamento dos pedidos, reduzindo interrupções na comunicação com a cozinha.

# Requisitos Funcionais

| Código | Requisito Funcional |
| --- | --- |
| RF01 | O sistema deve permitir que o administrador realize autenticação. |
| RF02 | O sistema deve permitir que o garçom realize autenticação utilizando suas credenciais. |
| RF03 | O sistema deve permitir ao administrador cadastrar usuários. |
| RF04 | O sistema deve permitir ao administrador alterar os dados dos usuários. |
| RF05 | O sistema deve permitir ao administrador alterar o status dos usuários. |
| RF06 | O sistema deve permitir consultar usuários cadastrados. |
| RF07 | O sistema deve restringir as funcionalidades de acordo com o perfil do usuário autenticado. |
| RF08 | O sistema deve permitir cadastrar categorias de itens. |
| RF09 | O sistema deve permitir alterar categorias de itens. |
| RF10 | O sistema deve permitir consultar categorias cadastradas. |
| RF11 | O sistema deve permitir cadastrar itens do cardápio. |
| RF12 | O sistema deve permitir alterar os dados dos itens do cardápio. |
| RF13 | O sistema deve permitir inativar itens do cardápio. |
| RF14 | O sistema deve permitir consultar itens cadastrados. |
| RF15 | O sistema deve permitir cadastrar mesas. |
| RF16 | O sistema deve permitir alterar a situação das mesas. |
| RF17 | O sistema deve permitir consultar mesas cadastradas. |
| RF18 | O sistema deve permitir criar pedidos para uma mesa. |
| RF19 | O sistema deve permitir adicionar um ou mais itens ao pedido. |
| RF20 | O sistema deve vincular automaticamente o pedido ao usuário autenticado. |
| RF21 | O sistema deve permitir alterar o status do pedido. |
| RF22 | O sistema deve permitir cancelar pedidos. |
| RF23 | O sistema deve permitir consultar o andamento dos pedidos. |
| RF24 | O sistema deve permitir consultar o histórico dos pedidos. |

# Requisitos Não Funcionais

| Código | Requisito Não Funcional |
| --- | --- |
| RNF01 | O sistema deve possuir uma interface intuitiva, permitindo que os usuários utilizem suas funcionalidades com treinamento mínimo. |
| RNF02 | O sistema deve responder às operações de cadastro, consulta e atualização em até 2 segundos, em condições normais de uso. |
| RNF03 | O sistema deve exigir autenticação antes do acesso às funcionalidades do sistema. |
| RNF04 | O sistema deve controlar o acesso às funcionalidades conforme o perfil do usuário autenticado. |
| RNF05 | O sistema deve registrar a data, a hora e o usuário responsável pelas operações realizadas nos pedidos. |
| RNF06 | O sistema deve garantir a integridade dos dados armazenados. |
| RNF07 | O sistema deve utilizar banco de dados relacional para persistência das informações. |
| RNF08 | O sistema deve suportar acesso simultâneo de múltiplos usuários sem comprometer o desempenho. |
| RNF09 | O sistema deve permanecer disponível durante todo o horário de funcionamento do restaurante. |
| RNF10 | O sistema deve apresentar mensagens de erro claras e objetivas aos usuários. |
| RNF11 | O sistema deve permitir futuras expansões e manutenções sem necessidade de reestruturação completa. |
| RNF12 | O sistema deve ser desenvolvido utilizando ASP.NET Core para o backend. |
| RNF13 | O sistema deve utilizar SQL Server como sistema gerenciador de banco de dados. |
| RNF14 | O sistema deve possuir interface desenvolvida utilizando React. |
| RNF15 | O sistema deve realizar a comunicação entre cliente e servidor por meio de API REST. |
| RNF16 | O sistema deve manter histórico das alterações de status dos pedidos, registrando data, hora e usuário responsável. |