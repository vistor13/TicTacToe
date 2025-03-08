export interface StartGameResponse
{
  id : number,
  Mode : string
}

export interface GameStateResponse {
  gameMode: string;
  state: string;
  grid: string[][];
  playerTurn: string;
}
