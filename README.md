# TriPeaks Solitaire

A mobile TriPeaks solitaire game built in Unity as a take-home assignment (August 2024).

Clear three overlapping pyramids of cards by tapping face-up cards that are one rank above or below the top card of the waste pile. Suits don't matter, and Aces and Kings connect, so runs can wrap around.

**Unity 2022.3.29f1** · uGUI · TextMeshPro · Android

## Running it

Open the project in Unity 2022.3.29f1, open `Assets/Scenes/Game.unity` and press Play.

## Features

- Three-peak board with 18 face-down and 10 face-up cards; covered cards flip once the cards above them are cleared
- Tap a face-up card to move it to the waste pile, or tap the stock pile to draw a new card
- Ace/King wrap-around for longer runs
- Mock purchase of a new deck, paid for with score, when the stock pile runs out
- Score, move counter and game timer in the HUD
- End-of-game leaderboard and high score, saved locally as JSON
- Animated dealing, card flips and card moves, with sound effects

## Technical approach

- **Board as a node graph.** Each board slot links to the slots above and below it, so the layout lives in the scene and other TriPeaks layouts need no code changes.
- **Event-driven systems.** Scoring, moves and the HUD react to gameplay events instead of being called by the rules code.
- **Data-driven card art.** Card sprites come from a ScriptableObject database, so card packs can be swapped in the Inspector.
- **On-device logging.** A custom log handler writes Unity's console output to a file for debugging builds on a device.

## Credits

- Playing cards by [Victor Meunier](https://www.victormeunier.com) (CC0)
- GUI elements from the "Silent" GUI asset by [Prinbles](https://prinbles.github.io)
- [Pixeloid](https://ggbot.net/fonts/) font by GGBotNet (SIL Open Font License 1.1)
- Card backs from the pack in `Assets/GraphicAssets/2D/CardsPack2` (see the EULA file in that folder)
