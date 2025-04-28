# EXP3 - Arthur Polak Furman, Gabriel Dias, Gabriel Furlan Mengarelli e Guilherme Alves

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

🔹 1. Remover os arquivos do versionamento, mas manter no seu computador
 ```
git rm --cached -r Pesadelo/Logs/
git rm --cached -r Pesadelo/UserSettings/
 ```
🔹 2. Fazer commit da remoção
 ```
git commit -m "Removendo arquivos de Logs e UserSettings do versionamento"
 ```
🔹 3. Enviar as mudanças para o repositório remoto
 ```
git push
 ```
🔹 4. Limpar arquivos não monitorados do seu diretório local
Use este comando para remover arquivos não rastreados que ainda estão no seu computador, evitando conflitos futuros ao mudar de branch.
 ```
git clean -fd
 ```

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

Mecânica do altar (Sacrifício de armas)

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
 
 ### 👀 Supostamentes 

Evolução de habilidades: Velocidade de ataque, Velocidade e Vida 

Bossfight 

## ⚒️ Itens

 | Armas | 1ª Parte | 2ª Parte | 3ª Parte |
 |------|-------|--------|--------|
 | Espada | Cabo | Lâmina | Gema |
 | Martelo | Cabo | Cabeça | Gema |
 | Cajado | Corpo | Ponta | Gema |

⚔️ Espada
Lâminas: 15

Guardas: 2

Cabos: 23

Gemas: 14

Combinações possíveis:
15 (lâminas) × 2 (guardas) × 23 (cabos) × 14 (gemas) = 9.660 espadas

🔨 Martelo
Cabeças: 15

Corpos: 15

Gemas: 14

Combinações possíveis:
15 (cabeças) × 15 (corpos) × 14 (gemas) = 3.150 martelos

💫 Cajado
Pontas: 15

Cabos: 15

Gemas: 14

Combinações possíveis:
15 (pontas) × 15 (cabos) × 14 (gemas) = 3.150 cajados

🧮 Total geral de armas
3.150 (cajados) + 3.150 (martelos) + 9.660 (espadas) = 15.960 armas únicas

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

Eventos: 

## 📚️📚️ Links de Auxílio

 | Aulas | Resumos |
 |------|---------|
 | Git LFS em Unity | - [Link](https://www.youtube.com/watch?v=_ewoEQFEURg) |
 | Arquivos base git | - [Link](https://www.patreon.com/posts/63076977) |
 | Unity - Collaborating with version control| [Link](https://learn.unity.com/tutorial/collaborate-with-plastic-scm#631f4f5dedbc2a27152629c3) |
 | Substituir arquivos locais | [Link](https://stackoverflow.com/questions/1125968/how-do-i-force-git-pull-to-overwrite-local-files) |
 


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

![littlenightmares](https://github.com/user-attachments/assets/46bd2138-b2b2-4160-b49f-3a192773f952)
![eldenmid](https://github.com/user-attachments/assets/12ff3fe5-0e67-4114-94c0-3a3953198cf0)
