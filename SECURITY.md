# Security Policy <!-- omit in toc -->

- [Version de Unity](#version-de-unity)
- [Comment collaborer sur un projet git](#comment-collaborer-sur-un-projet-git)
  - [Création de branch](#création-de-branch)
  - [Rebase Commit](#rebase-commit)
  - [Pull Request](#pull-request)
  - [Si Able to merge.](#si-able-to-merge)
  - [Si Can't automaticly merge.](#si-cant-automaticly-merge)
  - [Si vous avez commit sur main sans faire exprès](#si-vous-avez-commit-sur-main-sans-faire-exprès)
- [Nommenlacture](#nommenlacture)
  - [Dans le code](#dans-le-code)
- [Coder sur téléphone](#coder-sur-téléphone)

# Version de Unity

| Version                                                             | Supported |
| ------------------------------------------------------------------- | --------- |
| [2021.1.20f1](https://unity3d.com/fr/unity/whats-new/2021.1.20)     | oui       |
| Autres                                                              | à éviter  |

# Comment collaborer sur un projet git

## Création de branch

On ne code jamais sur la branche main/master, il faut toujours coder sur une autre branche.  

Pour créer une branche sur un projet GitHub :  
Allez dans l'onglet "Branch" --> "New Branch" (ou Ctrl+Shit+N)  

Nommez votre branche par votre nom  

Si vous avez deja fait des modifications avant de créer votre branch, git va vous proposer d'apporter vos changement sur la branche que vous venez de créer ou de les laisser sur la branche main.  

Vous pouvez a présent travallier sur votre branche.  

## Rebase Commit

Chaque jour, faites un "Rebase commit", pour vérifier que votre branche est à jour par rapport à main  

Dans github desktop, cliquer sur l'onglet "Branch", puis "Choose a branch to merge into [NomBranch]"  

Séléctionnez la branche "main", et sélectionner "Rebase commit"  

- [Si Able to merge.](#si-able-to-merge)  
- [Si Can't automaticly merge.](#si-cant-automaticly-merge)  

## Pull Request

Quand vous comptez intégrez vos fonctionnalités/assets au projet, il faut fusionner votre branche à la branche main (un merge)  

Pour ça il faut se rendre sur le projet GitHub en ligne (Ctrl+Shit+G dans github desktop)  

Si votre commit est récent cliquez sur "compare & pull request" sinon cliquez sur "X branches", cherchez le nom de votre branche et cliquez sur "New pull Request"

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

# Nommenlacture

CamelCase pour TOUS les fichiers et variables, aucun accents ou espaces  
exemple : maVariableTropStylee  

## Dans le code

| Variable | Nommenclature |
| -------- | ------------- |
| private  | _myVar        |
| public   | myVar         |

| Fonction | Nommenclature |
| -------- | ------------- |
| private  | MyFunction()  |
| public   | MyFunction()  |


# Coder sur téléphone
1. Utiliser l'application Unity Remote 5.
   - [Android](https://play.google.com/store/apps/details?id=com.unity3d.mobileremote&hl=fr&gl=US)  
   - [IOS](https://apps.apple.com/fr/app/unity-remote-5/id871767552)  

2. Activer le mode développeur du téléphone.

3. Installer dans la [Version actuelle](#version-de-unity) de Unity les modules "Android SDK & NDK Tools" et "OpenJDK".

4. Ouvrir le projet Unity, aller dans [Edit] > [Project Settings...] > [Editor] > [Device] et sélectionner "Any Android Device".

5. Branchez un cable USB au téléphone et au PC, appuyez sur Play, le jeu devrait s'afficher sur votre téléphone.