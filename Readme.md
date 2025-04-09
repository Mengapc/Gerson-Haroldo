# ExC3 - Arthur Polak Furman, Gabriel Dias, Gabriel Furlan Mengarelli e Guilherme Alves

Repositório relacionada ao projeto, contendo resumos sobre Git e GitHub e ideias sobre as features do jogo.

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
### 🔹 Atualizar sua branch com as mudanças da main (do remoto)
 ```
 git pull origin main
 ```
### 🔹 Trocar de branch
 ```
 git checkout <nome_da_branch>
 ```
 ### 🔹 Junta as mudanças da sua branch na main
 ```
 git merge <nome_da_branch>
 ```


## 📚️📚️ Links de Auxílio

 | Aulas | Resumos |
 |------|---------|
 | Git LFS em Unity | - [Link](https://www.youtube.com/watch?v=_ewoEQFEURg) |
 | Arquivos base git | - [Link](https://www.patreon.com/posts/63076977) |

 - [Documentação Git](https://git-scm.com/doc)
 - [Documentação GitHub](https://docs.github.com/)
 - [Github Material de Apoio](https://github.com/elidianaandrade/dio-curso-git-github)
 - [Apresentação Versionamento de Código](https://academiapme-my.sharepoint.com/:p:/g/personal/renato_dio_me/EYjkgVZuUv5HsVgJUEPv1_oB_QWs8MFBY_PBQ2UAtLqucg?rtime=FOF68ttW3Ug)


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

 - Ideia 3 foi escolhida 

 

 ## 💻️ Mecânicas 


Movimentação do personagem (Dash)

Nível base protótipo

Inimigos dropam as armas

Gemas (efeitos) de cada arma

IA (Inteligência artificial) dos inimigos 

Loja: você entrega dois itens de raridade baixa, para conseguir um item de raridade maior: 2 x 1

Ataque/comportamento de cada tipo das armas (martelo, espada e cajado) 

Cada inimigo spawna aleatoriamente com um desses tipos de arma 

Geração procedural do mapa 

Geração procedural de armas 

Raridades (baseado no nível de poder da arma) 

Separado em:  

Espada: cabo, lâmina e guarda 

Martelo: Cabo, cabeça e gema 

Cajado: Corpo, ponta e gema 

Habilidades das gemas 

Ataque de cada um dos tipos de arm 

| Gemas |
|--------|
| Fogo/Lava |
| Galático |
| Gelo/Frio |
| Vento/Ar |
| Veneno/Ácido |
| Pedra |
| Água/Mar |
| Ouro/Divino |
| Cristais/Pedras preciosas |
| Planta/Vinhas |
| Unicórnio/Arco íris |
| Pelúcia |
| Videogame/hacker |
| Gatinho |

 ## 🎮 Balanceamento 

 

Vida do player e inimigo 

Velocidade de ataque para o player e inimigo 

Cooldown de tomar e dar dano 

Velocidade de movimento do player e inimigo 

Habilidades 

Progressão de dificuldade de tudo acima 

Dificuldade das fases futuras é definido pela qualidade do sacrifício do nível anterior Diagrama

O conteúdo gerado por IA pode estar incorreto., Picture 

 

 

 ## 👀 Supostamentes 

Evolução de habilidades: Velocidade de ataque, Velocidade e Vida

JOGO SERÁ FINITO, com fim 

Loja 

Bossfight 

Para o MVP: Menu inicial com botão de play, créditos e sair  

Inteligência artificial de inimigos (pesquisar sobre navmesh) 

Mecânica do altar (Sacrifício de armas) 

 

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

Eventos: 

 ## 🔎 Inspirações e Referências

  - [Digital Innovation One](https://web.dio.me/home).

 | Jogos | Inspirou | Link |
 |------|---------| -------|
 | Bulb Boy | Inspiração | - [Link](https://store.steampowered.com/app/390290/Bulb_Boy/ ) |
 | Elden Ring | Inimigos | - [Link](https://store.steampowered.com/app/1245620/ELDEN_RING/) |
 | Little Nightmares | Player | - [Link](https://store.steampowered.com/app/424840/Little_Nightmares/ ) |

![littlenightmares](https://github.com/user-attachments/assets/46bd2138-b2b2-4160-b49f-3a192773f952)
![eldenmid](https://github.com/user-attachments/assets/12ff3fe5-0e67-4114-94c0-3a3953198cf0)
