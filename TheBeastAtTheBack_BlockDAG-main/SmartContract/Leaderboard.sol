// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

contract Leaderboard {
    struct ScoreEntry {
        address player;
        uint256 score;
        string username;
    }

    mapping(address => ScoreEntry) public leaderboard;
    address[] public players; // keeps track of unique players

    event ScoreSubmitted(address indexed player, uint256 score, string username);

    function submitScore(uint256 _score, string calldata _username) public {
        ScoreEntry storage entry = leaderboard[msg.sender];

        // New player → add them
        if (entry.player == address(0)) {
            leaderboard[msg.sender] = ScoreEntry(msg.sender, _score, _username);
            players.push(msg.sender);
            emit ScoreSubmitted(msg.sender, _score, _username);
        } else {
            // Existing player → only update if higher
            if (_score > entry.score) {
                leaderboard[msg.sender].score = _score;
                leaderboard[msg.sender].username = _username;
                emit ScoreSubmitted(msg.sender, _score, _username);
            }
        }
    }

    function getPlayerCount() public view returns (uint256) {
        return players.length;
    }

    function getPlayerAt(uint256 index) public view returns (address, uint256, string memory) {
        address p = players[index];
        ScoreEntry memory entry = leaderboard[p];
        return (entry.player, entry.score, entry.username);
    }
}
