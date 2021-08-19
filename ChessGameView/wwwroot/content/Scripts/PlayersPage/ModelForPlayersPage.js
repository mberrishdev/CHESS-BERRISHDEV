class Player {
    constructor(playerId, name, born, age, country) {
        this.playerId = playerId;
        this.name = name;
        this.born = born;
        this.age = age;
        this.country = country;
    }
    playerInformation() {

        return [this.playerId,
        this.name,
        this.born,
        this.age,
        this.country]

    }
}