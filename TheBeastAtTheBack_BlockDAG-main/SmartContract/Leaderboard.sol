// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

contract Leaderboard {
    struct ScoreEntry {
        address player;
        uint256 score;
        string username;
    }

    // Mapping of player → score entry
    mapping(address => ScoreEntry) public leaderboard;

    // Array of all unique player addresses
    address[] public players;

    event ScoreSubmitted(address indexed player, uint256 score, string username);

    /**
     * @notice Submit a new score or update existing score if higher.
     */
    function submitScore(uint256 _score, string calldata _username) external {
        ScoreEntry storage entry = leaderboard[msg.sender];

        // New player
        if (entry.player == address(0)) {
            leaderboard[msg.sender] = ScoreEntry(msg.sender, _score, _username);
            players.push(msg.sender);
            emit ScoreSubmitted(msg.sender, _score, _username);
            return;
        }

        // Existing player → update only if score is higher
        if (_score > entry.score) {
            entry.score = _score;
            entry.username = _username;
            emit ScoreSubmitted(msg.sender, _score, _username);
        }
    }

    /**
     * @notice Get total number of players.
     */
    function getPlayerCount() external view returns (uint256) {
        return players.length;
    }

    /**
     * @notice Get player data by index.
     */
    function getPlayerAt(uint256 index)
        external
        view
        returns (address player, uint256 score, string memory username)
    {
        address p = players[index];
        ScoreEntry memory entry = leaderboard[p];
        return (entry.player, entry.score, entry.username);
    }

    /**
     * @notice Returns all leaderboard entries in one call.
     * Useful for Unity/Web apps.
     */
    function getAllPlayers()
        external
        view
        returns (ScoreEntry[] memory)
    {
        uint256 count = players.length;
        ScoreEntry[] memory allScores = new ScoreEntry[](count);

        for (uint256 i = 0; i < count; i++) {
            allScores[i] = leaderboard[players[i]];
        }

        return allScores;
    }
}

