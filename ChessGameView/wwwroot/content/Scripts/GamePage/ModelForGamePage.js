var boardArray = [];
var possibleMoveArray = [];
var moveArray = [];
var whereGoArray = [];
var dangerousBoard = [];
var whereAllPieceCanGo = [];
var whereKingCanGoArray = [];
var maxLoop;
var maxstep;
var horizontalstep;
var verticalstep;
var array = [];


var whereGoArrayKing = [];

class Board {

    constructor(height, width) {
        this.height = height;
        this.width = width;
        this.board = this.imagineBoard();
    }

    imagineBoard() {

        for (var index = 0; index < this.height; index++) {

            var supporterArray = [];
            for (var verticalIndex = 0; verticalIndex < this.width; verticalIndex++) {
                supporterArray.push(0);
            }
            boardArray.push(supporterArray);
        }
        return boardArray;

    }
}


class Piece extends Board {

    constructor(name, color, height, width) {
        super(height, width)
        this.name = name;
        this.color = color;
    }

}

class Pawn extends Piece {

    constructor(name, color, horizontalCordinate, verticalCordinate) {

        super(name, color);
        this.horizontalCordinate = horizontalCordinate;
        this.verticalCordinate = verticalCordinate;
    }


}


class King extends Piece {

    constructor(name, color, horizontalCordinate, verticalCordinate) {

        super(name, color);
        this.horizontalCordinate = horizontalCordinate;
        this.verticalCordinate = verticalCordinate;

    }

}


class Queen extends Piece {

    constructor(name, color, horizontalCordinate, verticalCordinate) {

        super(name, color);
        this.horizontalCordinate = horizontalCordinate;
        this.verticalCordinate = verticalCordinate;
    }

}


class Rook extends Piece {

    constructor(name, color, horizontalCordinate, verticalCordinate) {

        super(name, color);
        this.horizontalCordinate = horizontalCordinate;
        this.verticalCordinate = verticalCordinate;
    }

}


class Bishop extends Piece {

    constructor(name, color, horizontalCordinate, verticalCordinate) {

        super(name, color);
        this.horizontalCordinate = horizontalCordinate;
        this.verticalCordinate = verticalCordinate;
    }

}


class Knight extends Piece {


    constructor(name, color, horizontalCordinate, verticalCordinate) {

        super(name, color);
        this.horizontalCordinate = horizontalCordinate;
        this.verticalCordinate = verticalCordinate;
    }


}

//Move information pair
class Pair {
    constructor(toHorizontal, toVertical, moveType, dangerChecker) {
        this.ToHorizontal = toHorizontal;
        this.TToVertical = toVertical;
        this.TMoveType = moveType;
        this.TDangerChecker = dangerChecker;
    }
}


class MakeMovePair {
    constructor() {
    }
    Create(ToHorizontal, ToVertical, MoveType, DangerChecker) {
        return new Pair(ToHorizontal, ToVertical, MoveType, DangerChecker);
    }
}