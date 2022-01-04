# Security Policy <!-- omit in toc -->

- [Version de Unity](#version-de-unity)
- [Comment collaborer sur un projet git](#comment-collaborer-sur-un-projet-git)
  - [Création de branch](#création-de-branch)
  - [Pull Request](#pull-request)
  - [Si Able to merge.](#si-able-to-merge)
  - [Si Can't automaticly merge.](#si-cant-automaticly-merge)
  - [Si vous avez commit sur main sans faire exprès](#si-vous-avez-commit-sur-main-sans-faire-exprès)

# Version de Unity

| Version                                                             | Supported |
| ------------------------------------------------------------------- | --------- |
| [2021.1.20f1](https://unity3d.com/fr/unity/whats-new/2021.1.20)     | oui       |
| Autres                                                              | à éviter  |

# Comment collaborer sur un projet git

## Création de branch

On ne code jamais sur la branche main/master, il faut toujours coder sur une auter branche.

Pour créer une branche sur un projet GitHub :  
aller dans l'onglet "Branch" --> "New Branch" (ou Ctrl+Shit+N)  

Nommez votre branch en fonciton de ce que vous voulez ajouter au projet comme une fonctionnalité ou des assets : (ex : personnages3D...)  

Si vous avez deja fait des modifications avant de créer votre branch, git va vous proposer d'apporter vos changement sur la branche que vous venez de créer ou de les laisser sur la branche main.  

Vous pouvez a présent travallier sur votre branche.  

## Pull Request

Vous avez finalisé ce que vous voulez intégrer au projet, maintenant il faut fusionner votre branche à la branche main (un merge)  

Il faut se rendre sur le projet GitHub en ligne (Ctrl+Shit+G dans github desktop)  

Si votre commit est récent cliquez sur "compare & pull request" sinon cliquez sur "X branches" (ici 2 branches sur l'exemple) cherchez le nom de votre branche et cliquez sur "New pull Request"

- [Si Able to merge.](#si-able-to-merge)  
- [Si Can't automaticly merge.](#si-cant-automaticly-merge)  

Créer votre Pull Requests dans les deux cas.

## Si Able to merge.

Vous pouvez directement merge votre pull request  

## Si Can't automaticly merge.

Ouvrez avec gitHub desktop pour réparer le conflit  

Cliquez sur votre branche dans les onglets en haut puis allez sur "Pull Request"  

Choisisez avec quelle branche vous voulez faire votre merge (en générale "main") et cliquez sur "Create a merge commit"  

Choisisez votre éditeur de texte et cliquez sur son nom pour l'ouvir  

Réparez le conflit.  
Une fois le conflit réparé vous pouvez commit + push.  

Pensez a finalisez votre merge sur GitHub en ligne  

Vous avez finalisé ce que vous voulez intégrer au projet, pour ça il faut fusionner votre branche à la branche main (un merge)  

## Si vous avez commit sur main sans faire exprès

Utiliser l'extension "[Git Graph](https://marketplace.visualstudio.com/items?itemName=mhutchie.git-graph)" sur [VScode](https://code.visualstudio.com/), sélectionner un commit, cliquer sur "Revert...", puis commit votre revert commit.

Egalement possible avec [SourceTree](https://www.sourcetreeapp.com/), sélectionner un commit, cliquer sur "Reverse commit...", puis commit votre revert commit.
