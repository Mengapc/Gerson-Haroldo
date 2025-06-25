# 🧸🪄👻 EXP3 - Eli: Into Escapism

![MenuEli](https://github.com/user-attachments/assets/985e4c86-ccfd-4663-b9d5-8ce782495324)

## 👥 Equipe de Desenvolvimento – Weird Milk Studios

![Captura de tela de 2025-06-25 12-02-18](https://github.com/user-attachments/assets/30802ecc-359a-4328-9e86-d3210a1d8477)

### Arthur Polak Furman  
**Função:** Programador e VFX  

Desenvolveu os sistemas dos inimigos e seu spawn. Além disso, criou os efeitos base das armas do jogo.

---

![Captura de tela de 2025-06-25 11-58-38](https://github.com/user-attachments/assets/49a98530-e988-4097-93a3-0ab484b31e82)

### Gabriel Dias  
**Função:** Programador e Game Designer  

Responsável pelas funcionalidades de inventário, geração procedural de armas e programação das mecânicas de habilidades.

---

![Captura de tela de 2025-06-25 12-01-00](https://github.com/user-attachments/assets/44cc162b-24cd-430d-a749-466f37ad6278)

### Gabriel Furlan Mengarelli                                                                                                                            
**Função:** Programador e Sonorizador  

Desenvolveu a geração procedural do mapa, o sistema do player, o core loop do jogo, a música do menu principal e a documentação do GitHub.

---

![Captura de tela de 2025-06-25 12-00-25](https://github.com/user-attachments/assets/dd7e0c4e-e1df-438f-97e5-652acb825986)

### Guilherme Alves  
**Função:** Artista 3D e VFX  

Criou todas as artes e modelos 3D utilizados no jogo, além dos efeitos visuais das habilidades especiais das armas. Também foi responsável pela imagem do menu.

---

**Estúdio:** Weird Milk Studios

👾 Bem-Vindo(a) ao Repositório do projeto **"Eli: into Escapsm"**
Este repositório funciona como um **GDD(Game Desing Document)** vivo reunindo todas as informações, ideias, sistemas e anotações que fazem parte do desenvolvimento do nosso jogo.

Aqui você vai encontrar:

- 🛠️ **Guia de instalação e execução**
- 🗂️ **Estrutura de pastas e scripts principais**
- 🕹️ **Descrição do projeto**
- 🧠 **Conceitos de gameplay e narrativa**
- 🐛 **Roadmap e problemas conhecidos**
- 📚 **Tutoriais, links úteis e referências**

Nosso objetivo é fazer um jogo com o tema **"Gambito"**, onde o jogador precisa sacrificar suas armas para avançar, assim modificando aspectos das fases seguintes.

# 📦 Instalação e Configuração

## Requisitos

| Item | Versão |
|------|--------|
| Unity Editor | 6000.0.35f1 |
| Render Pipeline | Universal Render Pipeline (URP) |
| Git | 2.47.1 |
| Git LFS | Instalado (`git lfs install`) |

### Passo a passo

1. Clone o repositório:
   ```bash
   git clone https://github.com/<org>/eli-into-escapism.git
   cd eli-into-escapism
   git lfs install
   git lfs pull

2. Abra o projeto no Unity (6000.0.35f1)

3. Cena inicial:

       · Assets/Scenes/testeprocedura.unity

4. Recomendado: build manual via File > Build Settings

# Documentação - Links, tutoriais, etc.

 ## 💻️ Comandos do git

### 🔹 Inicializar repositório
 ```
 git init
 ```
### 🔹 Adicionar arquivos para commit
 ```
 git add .
 ```
### 🔹 Fazer commit com mensagem
 ```
 git commit -m "Nome_commit"
 ```
### 🔹 Enviar mudanças para o repositório remoto (main)
 ```
 git push -u origin main
 ```
### Após a primeira vez, você pode usar apenas:
 ```
 git push
 ```
### 🔹 Atualizar sua branch com as mudanças da main (do remoto)
 ```
 git pull origin main
 ```
### Após a primeira vez, você pode usar apenas:
 ```
 git pull
 ```
### 🔹 Trocar de branch
 ```
 git checkout <nome_da_branch>
 ```
 ### 🔹 Junta as mudanças da sua branch na main
 ```
 git merge <nome_da_branch>
 ```
Exemplo: Se estiver na main e rodar git merge minha-branch, as mudanças da minha-branch serão aplicadas na main.

## ⚠ Lidando com conflitos de merge
### 🔹 Abortar um merge com conflitos

Se durante um merge houver conflitos e você quiser cancelar o processo:
 ```
 git merge --abort
 ```

### 🔹 Manter somente os arquivos da branch atual
Caso você queira forçar o Git a manter os arquivos da branch atual durante um merge, ignorando os da outra branch:
 ```
 git merge <nome-da-branch> --strategy-option=ours
 ```
- Isso resolve todos os conflitos automaticamente, mantendo a versão da branch em que você está.
Em seguida, finalize com:
 ```
git commit -m "Merge mantendo arquivos da branch atual"
 ```

### 🔹 Sobre conflitos de merge e UserSettings
Arquivos como Pesadelo/UserSettings/Layouts/default-6000.dwlt costumam causar conflitos de merge.

🧹 Remover arquivos já versionados e ignorá-los corretamente
Se você adicionou pastas como Logs ou UserSettings no .gitignore depois que elas já estavam sendo versionadas, siga o passo-a-passo abaixo para limpar o repositório:

### 🔹 1. Remover os arquivos do versionamento, mas manter no seu computador
 ```
git rm --cached -r Pesadelo/Logs/
git rm --cached -r Pesadelo/UserSettings/
 ```
### 🔹 2. Fazer commit da remoção
 ```
git commit -m "Removendo arquivos de Logs e UserSettings do versionamento"
 ```
### 🔹 3. Enviar as mudanças para o repositório remoto
 ```
git push
 ```
### 🔹 4. Limpar arquivos não monitorados do seu diretório local
Use este comando para remover arquivos não rastreados que ainda estão no seu computador, evitando conflitos futuros ao mudar de branch.
 ```
git clean -fd
 ```
### 🔹 Como resolver conflito aceitando o arquivo da outra branch
Se durante um conflito você quiser pegar o arquivo da outra branch e descartar a sua versão, use:
 ```
git checkout --theirs "caminho/do/arquivo"
git add "caminho/do/arquivo"
git commit
 ```
Exemplo para o seu projeto:
 ```
git checkout --theirs "Pesadelo/Assets/testeprocedura.unity"
git add "Pesadelo/Assets/testeprocedura.unity"
git commit -m "Resolvendo conflito aceitando versão da outra branch"
 ```

Explicação rápida:
 - --theirs = pega a versão da branch que você está puxando/mesclando.
  
 - --ours = pega a sua versão atual.

# 🗂️ Estrutura de Pastas

Assets/

├── Data/                      ← ScriptableObjects e parâmetros de geração

├── Editor/                    ← Scripts customizados de Editor

├── Executores/                ← Objetos de cena (luz, canvas, dungeons)

├── Materials/                 ← Materiais de armas e efeitos

├── Prefabs/                   ← Prefabs do jogo (armas, estruturas, VFX)

├── Scenes/                    ← Cenas (principal: testeprocedura.unity)

├── Script/

│   ├── Enemy/                 ← IA e comportamento inimigo

│   ├── Geração de Armas/      ← Montagem procedural

│   ├── Interactable_objects/  ← Itens interativos

│   ├── InventárioDropArmas/   ← Drop e inventário

│   ├── Player/                ← Controle do jogador

│   ├── Procedural_Map/        ← Geração de mapa

│   └── System/                ← Auxiliares gerais

├── TextMesh Pro/

├── TutorialInfo/

├── VFX's/

├── InputSystem_Actions.inputactions

├── URP/

└── URP_Renderer.asset

# 📜 Organização do Código

## Scripts principais

| Script                       | Função                                           |
| ---------------------------- | ------------------------------------------------ |
| RoomFirstDungeonGenerator.cs | Gera o mapa procedural com dicionário de tiles.  |
| PlayerInteractionTrigger.cs  | Gerencia interações do player com salas e itens. |
| SkillsManager.cs             | Aplica efeitos da arma com base em tipo e gema.  |
| EnemySistem.cs               | Mecânica base do inimigo padrão.                 |
| ProceduralItens.cs           | Monta armas com partes combináveis.              |

### Lógicas aplicadas

· Armas montadas via Struct com 9 arrays:

      · Tipo (espada, cajado, martelo)
      · Gema (vento, água, galáxia)
      · Prefabs específicos de ataque (Ex: Martelo_Vento)

Sistema de mapa procedural:

    · Armazena os tiles instanciados num Dictionary<Vector3, GameObject>
    · Usa Clear() ao fim da fase para resetar o mapa

Sistema de habilidades:

    · SkillsManager armazena um dicionário de funções por tipo de arma
    · Ativação dinâmica conforme ataque executado

# 🎮 Build & Execução

## Plataformas

### 🖥️ Windows (.exe)

### 🌐 WebGL (previsto para Itch.io)

## Passos para build

1. File > Build Settings

2. Selecione a plataforma desejada

3. Clique em Add Open Scenes e inclua testeprocedura.unity

4. Configure se quiser em Player Settings

5. Clique em Build

_Não há configurações personalizadas no Player Settings._

# Tema e Mecânica

Tema escolhido: Gambito

Mecânicas únicas:
· A mecânica única do jogo está relacionada ao tema Gambito, onde o jogador deve sacrificar sua arma para avançar os níveis

· Outra mecânica única que existe no jogo é o altar do sacrifício, na qual o jogador escolhe "reciclar" armas coletadas, com o objetivo de receber uma arma melhor com uma raridade melhor.

Câmera: Isométrica / Top Down

 ## 📚 História

 | Ideias | Resumos |
 |--------| --------------- |
 |  1  | - Um menino no halloween vestido de super-herói entra sem querer em uma casa de sustos procurando por doces batendo porta em porta. O jogo se passa dentro dessa casa de sustos e o menino está imaginando todos os inimigos dentro da casa lutando com os inimigos para escapar.|
 |  2  | - Um menino foi para uma casa de susto no halloween, tomou um susto enorme, sofreu uma parada cardíaca foi parar no hospital e agora você está jogando dentro do sonho de coma, onde ele tem pesadelos com o que viu dentro da casa, distorcidos e assustadores. | 
 |  3  | - Mesmo menino está tendo um pesadelo numa noite de halloween, ou seja, ele sonha em ser um herói e está lutando contra seus medos. | 

 ### ⭐️⭐️ Ideia 3 foi escolhida
 
## 💻️ Mecânicas
Geração procedural do mapa

Geração procedural de armas

Efeitos para cada arma

Ataque/comportamento de cada tipo de arma, baseado em cada gema

Mecânica do altar (sacrifício de armas)

Movimentação do personagem (dash)

### 👀 Supostamentes
Evolução de habilidades: velocidade de ataque, velocidade e vida

Loja: entregue dois itens de raridade baixa para obter um de raridade maior (2x1)

IA (inteligência artificial) dos inimigos

Bossfight

## ⚒️ Itens

 | Armas | 1ª Parte | 2ª Parte | 3ª Parte |
 |------|-------|--------|--------|
 | Espada | Cabo | Lâmina | Gema |
 | Martelo | Cabo | Cabeça | Gema |
 | Cajado | Corpo | Ponta | Gema |

### MVP

⚔️ Espada
Lâminas: 15

Guardas: 2

Cabos: 23

Gemas (MVP): 4

Combinações possíveis (MVP):
15 (lâminas) × 2 (guardas) × 23 (cabos) × 4 (gemas) = 2.760 espadas

🔨 Martelo
Cabeças: 15

Corpos: 15

Gemas (MVP): 4

Combinações possíveis (MVP):
15 (cabeças) × 15 (corpos) × 4 (gemas) = 900 martelos

💫 Cajado
Pontas: 15

Cabos: 15

Gemas (MVP): 4

Combinações possíveis (MVP):
15 (pontas) × 15 (cabos) × 4 (gemas) = 900 cajados

🧮 Total geral de armas (MVP)
900 (cajados) + 900 (martelos) + 2.760 (espadas) = 4.560 armas únicas

### ℹ️ Observação: Estes valores correspondem à versão MVP do jogo, com apenas 4 gemas disponíveis. A versão completa está planejada para incluir 14 gemas, totalizando 15.960 armas únicas no futuro.

## ✨ Raridades

| | Tipos |
|--|--------|
|1| Comum |
|2| Incomum |
|3| Raro |
|4| Épico |
|5| Lendário |


### 💎 Habilidades das gemas

| Gemas |
|--------|
| Vento/Ar |
| Água/Mar |
| Galático |
| Gatinho |
| Fogo/Lava |
| Gelo/Frio |
| Veneno/Ácido |
| Pedra |
| Ouro/Divino |
| Cristais/Pedras preciosas |
| Planta/Vinhas |
| Unicórnio/Arco íris |
| Pelúcia |
| Videogame/hacker |

## Comportamentos das Armas

## _**Gema do Vento**_
Efeito: inimigos sofrem knockback (recuo).

 | Armas | Descrição Habilidade |
 |------|--------------------------------------------------------------------|
 | Espada | Lança uma rajada com efeito de vento. |
 | Martelo | Martelada com efeito de empurrão. |
 | Cajado | Dispara um tornado que avança e para no final da trajetória. |

## _**Gema da Água**_
Efeito: inimigos recebem slow (lentidão).

 | Armas | Descrição Habilidade |
 |------|--------------------------------------------------------------------|
 | Espada | Lança uma rajada com efeito de água. |
 | Martelo | Martelada com efeito de lentidão. |
 | Cajado | Dispara uma onda que para no final da trajetória e deixa uma poça que reduz a velocidade dos inimigos. |

## _**Gema da Galáxia**_
Efeito: inimigos são atraídos para um ponto.

 | Armas | Descrição Habilidade |
 |------|--------------------------------------------------------------------|
 | Espada | Lança uma rajada com efeito gravitacional. |
 | Martelo | Martelada que cria um buraco negro, atraindo inimigos e causando dano contínuo. |
 | Cajado | Dispara um buraco negro que para no final da trajetória, atraindo inimigos e causando dano com partículas cósmicas. |

## _**Gema de Gatinho**_
Efeito: explosão.

 | Armas | Descrição Habilidade |
 |------|--------------------------------------------------------------------|
 | Espada | Lança um projétil que explode ao atingir, liberando partículas que causam dano em inimigos próximos. |
 | Martelo | Martelada explosiva que causa dano em área. |
 | Cajado | Dispara um projétil que explode no final da trajetória, causando dano em uma área ampla. |

 ## 🎮 Balanceamento 

### 1. Player
##### 1.1 Vida
 - Quantidade de dano que o jogador pode receber antes de ser derrotado.
##### 1.2 Velocidade de ataque
 - Frequência com que o jogador pode desferir ataques.
##### 1.3 Velocidade de movimento
 - Rapidez com que o jogador se move pelo cenário.
##### 1.4 Habilidades 
 - Poderes ou ações especiais que o jogador pode utilizar em combate ou exploração.
### 2. Inimigos
##### 2.1 Vida
 - Resistência dos inimigos ao dano causado pelo jogador.
##### 2.2 Velocidade de ataque
 - Tempo entre um ataque e outro dos inimigos.
##### 2.3 Velocidade de movimento
 - Rapidez com que os inimigos se deslocam e perseguem o jogador.
### 3. Sistemas
##### 3.1 Cooldown de tomar e dar dano
 - Intervalo mínimo entre um golpe recebido ou causado, evitando spam de dano.
##### 3.2 Progressão de dificuldade
 - Sistema que torna o jogo gradualmente mais desafiador conforme o avanço do jogador.
##### 3.3 Sistema de Sacrifício e Progressão – Mecânica de Jogo
 - Sistema central que liga narrativa e gameplay por meio de escolhas estratégicas.

 - Conceito central:
A dificuldade da próxima fase é determinada pela raridade da arma sacrificada ao final da fase anterior.

 - Mecânica:
Ao final de cada fase, o jogador deve sacrificar uma arma em um altar sagrado.
Esse sacrifício é obrigatório para acessar a próxima fase.
A raridade da arma sacrificada influencia diretamente na dificuldade da fase seguinte:

Armas comuns → próxima fase muito mais difícil.                                       
Armas raras ou lendárias → próxima fase mais equilibrada.

- O jogador deve decidir entre:
Manter uma boa arma para lutar mais fácil agora, e sofrer depois.
Ou abrir mão de uma arma poderosa agora, para garantir um caminho mais seguro depois.
 
 ## 👿 Inimigos 

### Medos (inimigos) 

### MORTE (várias versões) 

 | Tipo | Descrição |
 |------|---------|
 | Afogamento | (Cabeça de barril) suga o player para perto e tenta afogar ele com sua cabeça (barril) cheia de água. |
 | Queda alta | Uma morte grande que arremessa o jogador para cima. |
 | Escuro | Bicho sombra genérico. |
 | Esmagamento | Pilar que pula em cima de player. |
 

### Ideias de boss

Medo do futuro: Boss final do nível do medo da solidão, seria o próprio jogador mais velho e no futuro com qual ele deve lutar
Mdeo do escuro: Boss final do nível do medo do escuro 

 ## 💻️ Ideias de nível

 | Tipo | Descrição |
 |------|---------|
 | Medo de solidão | Não há nenhum inimigo na tela, apenas vários bonecos de pelúcia que te ajudam. |
 | Medo do escuro | Nível com visão limitado devido a ausência da luz. |

# 🚧 Roadmap & Problemas Conhecidos

## Funcionalidades

| Status | Item                | Descrição                                    |
| ------ | ------------------- | -------------------------------------------- |
| ✅      | Altar de Sacrifício | Mecânica principal que ajusta a dificuldade. |
| ✅      | Geração Procedural  | Mapas e armas com combinações únicas.        |
| ⚙️     | Mecânicas de Armas  | Algumas feitas, outras planejadas.           |
| 🔜     | Loja 2x1            | Troca de dois itens por um melhor.           |
| 🔜     | IA Variada          | Novos padrões de inimigos.                   |
| 🔜     | Bossfights          | Temáticos, como “medo do escuro”.            |
| 🔜     | Estéticas de Fase   | Ex: fase com visibilidade reduzida.          |

## Bugs Conhecidos

| Área      | Problema            | Descrição                         |
| --------- | ------------------- | --------------------------------- |
| Knockback | Força instável      | Pode jogar inimigos fora do mapa. |
| Player    | Rotação na morte    | Personagem revive deitado.        |
| Dash      | Player fora do mapa | Há respawn, mas precisa melhoria. |

## Organização

· Código-fonte: GitHub

![25231](https://github.com/user-attachments/assets/404d27a5-61c0-4625-bbff-45b3bf03d08c)

· Tarefas: Hack n Plan

![images](https://github.com/user-attachments/assets/cb43d901-921f-4dbd-a7a6-b34029d296b2)

## 📚️📚️ Links de Auxílio

 | Aulas | Resumos |
 |------|---------|
 | Git LFS em Unity | - [Link](https://www.youtube.com/watch?v=_ewoEQFEURg) |
 | Arquivos base git | - [Link](https://www.patreon.com/posts/63076977) |
 | Unity - Collaborating with version control| - [Link](https://learn.unity.com/tutorial/collaborate-with-plastic-scm#631f4f5dedbc2a27152629c3) |
 | Substituir arquivos locais | - [Link](https://stackoverflow.com/questions/1125968/how-do-i-force-git-pull-to-overwrite-local-files) |
 


 - [Digital Innovation One](https://web.dio.me/home).https://learn.unity.com/tutorial/collaborate-with-plastic-scm#631f4f5dedbc2a27152629c3
 - [Documentação Git](https://git-scm.com/doc)
 - [Documentação GitHub](https://docs.github.com/)
 - [Github Material de Apoio](https://github.com/elidianaandrade/dio-curso-git-github)
 - [Apresentação Versionamento de Código](https://academiapme-my.sharepoint.com/:p:/g/personal/renato_dio_me/EYjkgVZuUv5HsVgJUEPv1_oB_QWs8MFBY_PBQ2UAtLqucg?rtime=FOF68ttW3Ug)

### 🐛🐛 Resolução de bugs

 - [Git branches bug](https://graphite.dev/guides/git-branch-not-showing-all-branches).

 ## 🎬️🎬️ Vídeos de Auxílio
 
 ### 1 - [Mapa Procedural 2D](https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&pp=0gcJCV8EOCosWNin)
 
[![Watch the video](https://i.sstatic.net/Vp2cE.png)](https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&pp=0gcJCV8EOCosWNin)
 
 ### 2 - [CineMachine](https://www.youtube.com/watch?v=wB-EQH7jvFY)
 
[![Watch the video](https://i.sstatic.net/Vp2cE.png)](https://www.youtube.com/watch?v=wB-EQH7jvFY)


 
 ## 🔎 Inspirações e Referências

 | Jogos | Inspirou | Link |
 |------|---------| -------|
 | Bulb Boy | Inspiração | - [Link](https://store.steampowered.com/app/390290/Bulb_Boy/ ) |
 | Elden Ring | Inimigos | - [Link](https://store.steampowered.com/app/1245620/ELDEN_RING/) |
 | Little Nightmares | Player | - [Link](https://store.steampowered.com/app/424840/Little_Nightmares/ ) |
 | HADES | Gameplay | - [Link](https://store.steampowered.com/app/1145360/Hades/) |

![littlenightmares](https://github.com/user-attachments/assets/46bd2138-b2b2-4160-b49f-3a192773f952)
![eldenmid](https://github.com/user-attachments/assets/12ff3fe5-0e67-4114-94c0-3a3953198cf0)
![Captura de tela de 2025-06-25 12-09-04](https://github.com/user-attachments/assets/8030ae11-787e-4009-866f-e0796cab5d8e)
