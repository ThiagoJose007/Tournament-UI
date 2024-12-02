# Tournament-UI
Criação de UIs para Teste Técnico de Programador UI para a empresa Heimo Games Studio.

# UI System - Tournament 

## Introdução
Este projeto utiliza o UI Toolkit para criar interfaces e configurações. O objetivo é proporcionar uma experiência visual coesa e responsiva com base nos layouts passados pela equipe no figma.

## Estrutura de Pastas
/UI/
Screens/         # Telas completas do jogo (ex.: Menu Principal, HUD)
Components/      # Elementos reutilizáveis (ex.: botões, sliders)
Styles/          # Arquivos USS para estilos globais e específicos
Templates/       # Templates UXML reutilizáveis
Icons/           # Ícones e imagens para a interface
Scripts/         # Lógica e controle de UI em C#

## Tecnologias
- Unity 6000.0.28f1
- UI Toolkit

## Como Contribuir
- Clone o repositorio
- Abra o UnityHub
- Baixe a versão Unity 6000.0.28f1
- Clique em Abrir -> Adicionar projeto do disco
- Selecione o projeto que você clonou
- Clique sobre o nome do projeto para abrir
- Agora você pode editar e adicionar novas funcionalidades 


Durante o teste para a vaga de UI Programmer, utilizei Unity em conjunto com UIToolkit e UIBuilder, empregando USS, UXML e C# para a criação das interfaces. Desde o início, busquei criar uma estrutura prática e escalável. Identifiquei que diversos componentes poderiam ser reutilizados e, por isso, optei por uma abordagem de componentização com Instantiate() em vez de Clone(), considerando sua maior flexibilidade para gerar, configurar e rastrear itens dinamicamente.

Para organizar os elementos, desenvolvi componentes separados e os integrei com ScriptableObjects que gerenciavam os dados dos itens (textos e imagens). Por meio de métodos como Q<>, localizei e manipulei os elementos na interface, permitindo instâncias eficientes e customizáveis no container designado. Essa estrutura facilitou o processo de personalização e reaproveitamento em diferentes cenários da aplicação.

Apesar de já ter experiência na criação de sites com HTML, CSS e JavaScript, enfrentei desafios com a responsividade no sistema do Unity, que não suporta unidades como vh, vw e rem. Resolvi isso ajustando manualmente cada componente e container com unidades percentuais (%), garantindo que a interface fosse flexível e se adaptasse a diferentes resoluções.

Outro ponto que merece destaque foi a criação de um sistema de categorias dinâmicas. Com uma lista de categorias e seus respectivos itens, implementei um mecanismo que exibe apenas os elementos correspondentes ao botão selecionado. Esse sistema, além de tornar a interface mais intuitiva, possibilita uma gestão eficiente dos elementos, permitindo sua reprodução nos containers adequados.

Por fim, a experiência reforçou meu entendimento do UIToolkit e UIBuilder, ferramentas que anteriormente não utilizava frequentemente, mas que demonstraram grande potencial para interfaces escaláveis e reutilizáveis. Apesar das dificuldades iniciais, o aprendizado e os ajustes realizados solidificaram meu domínio sobre essas tecnologias, alinhando praticidade, performance e organização na criação de interfaces.

**Desafios:**

Um dos principais desafios enfrentados foi o **tempo limitado**, pois inicialmente planejava desenvolver mais de uma tela, mas precisei adaptar o escopo. Outro ponto foi a **falta de domínio completo sobre o UI Toolkit e UI Builder**, que exigiu esforço adicional para compreender conceitos e funcionalidades específicas. Para superar isso, fiz um **planejamento detalhado**, aplicando minha experiência em **gestão de projetos** para definir prioridades e organizar as tarefas com base nos requisitos fornecidos. Utilizei meus conhecimentos em **C#** para criar scripts que automatizassem a geração de itens, evitando trabalho manual. Além disso, dediquei-me a consumir o máximo de **conteúdo educacional**, como vídeos e artigos, sobre o tema para otimizar o resultado dentro do prazo estipulado.

**Porque utilizar UIToolKit?**
O **UI Toolkit** é uma escolha superior ao **UGUI** para este projeto devido à sua abordagem baseada em **DOM** e **CSS**, o que facilita a criação de interfaces modulares e reutilizáveis. Ele oferece melhor **performance**, principalmente para interfaces complexas e dinâmicas, pois reduz a sobrecarga causada pela hierarquia de objetos no Canvas do UGUI. Além disso, o UI Toolkit promove maior **escalabilidade**, permitindo que componentes sejam facilmente estilizados e gerenciados com USS, além de proporcionar maior integração com sistemas modernos, como bindings de dados e workflows baseados em código.

## Referências
- [Documentação do UI Toolkit](https://docs.unity3d.com/Manual/UIElements.html)
