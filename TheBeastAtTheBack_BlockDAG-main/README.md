# 🐺 The Beast at the Back – BlockDAG

An endless horror runner where you’re being chased by **The Beast** through a dark, cartoony nightmare. Jump, dodge, survive, and **prove your skill on-chain!**

🎮 **Play now (itch.io):** [https://midhan.itch.io/the-beast-at-the-back](https://midhan.itch.io/the-beast-at-the-back)
📜 **Leaderboard:** [On-Chain Leaderboard](https://midhanraj.github.io/TheBeastAtTheBack_BlockDAG/Leaderboard_Web/Leaderboard.html)

⚠️ *Note: GitHub Pages cannot serve compressed Unity WebGL files correctly (Brotli/Gzip issue). For the best experience, play on itch.io.*

---

## 🌟 Features

✅ Endless horror runner with smooth keyboard/touch controls
✅ On-chain **leaderboard** stored on BlockDAG’s Primordial Testnet
✅ **MetaMask wallet integration** for score submission
✅ Highscores are saved **only if they beat your previous record**
✅ Jumpscare when you lose 😱
✅ Cartoony but eerie dark aesthetic

---

## 🕹 How to Play

1. **Press Space or Tap** to jump.
2. **Avoid rocks and traps** to keep running.
3. The Beast keeps getting closer… don’t get caught!
4. After Game Over, your score is sent to the **Leaderboard** page.
5. Connect your wallet → choose a username → submit your best score → see yourself ranked!

---

## 📂 Repo Structure

```
TheBeastAtTheBack_BlockDAG/
│
├── WebGL_Build/         # Unity WebGL build files
│   ├── index.html
│   ├── Build/
│   └── TemplateData/
│
├── Leaderboard_Web/     # Leaderboard HTML + JS for wallet connect
│   ├── Leaderboard.html
│   └── ethers.min.js
│
├── SmartContract/       # Solidity smart contract + ABI + address
├── Unity_Scripts/       # Core Unity scripts
├── Docs/                # Project summary, PPT, screenshots
│
├── LICENSE              # MIT License
└── README.md            # You are reading this
```

---

## 🔗 Smart Contract

* **Network:** BlockDAG Primordial Testnet
* **Contract Address:** `0x7b41e89fd20bec81adbaad81c7e0ee2bd409f402`
* **Features:**
  ✅ Saves your **best score + username**
  ✅ Prevents duplicates unless you beat your previous highscore
  ✅ Fully transparent on-chain

---

## 🚀 Setup for Developers

```bash
git clone https://github.com/MidhanRaj/TheBeastAtTheBack_BlockDAG.git
```

You can modify:

* `Unity_Scripts/` for gameplay changes
* `SmartContract/` for contract upgrades
* `Leaderboard_Web/` to improve UI

---

## 🔗 Demo Links

🎮 **Playable Game:** [itch.io link](https://midhan.itch.io/the-beast-at-the-back)
📜 **Leaderboard:** [GitHub Pages](https://midhanraj.github.io/TheBeastAtTheBack_BlockDAG/Leaderboard_Web/Leaderboard.html)
▶️ **Demo Video:** [YouTube link](https://www.youtube.com/watch?v=so26lqM3IF0)

---

## 🛠 Built With

* **Unity WebGL** – Endless runner game
* **Solidity** – Smart contract for on-chain leaderboard
* **ethers.js + MetaMask** – Wallet & score submission
* **GitHub Pages** – Leaderboard hosting
* **itch.io** – Playable demo hosting

---

## 📈 Future Scope

* Multi-game shared leaderboard
* On-chain rewards for top players
* Multiplayer on-chain games
* Shop system & cosmetic upgrades
* More obstacles + crouching mechanics

---

## ❓ Why can’t you play directly on GitHub Pages?

Unity WebGL build uses **Brotli compression**. GitHub Pages doesn’t send the correct HTTP headers (`Content-Encoding: br`), so the game fails to load.
➡ **Solution:** Hosted on **itch.io** where correct headers are served.

---

### ✅ **So you can:**

* **Play on itch.io** (recommended): https://midhan.itch.io/the-beast-at-the-back
* Use **GitHub Pages ONLY for leaderboard**
