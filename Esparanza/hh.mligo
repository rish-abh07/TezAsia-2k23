type nft = {
  token_id: nat;
  owner: address;
  name: string;
  description: string;
}

type game_data = {
  hint: string;
  attempts: nat;
  time_started: timestamp;
  place_choice: string;
  is_active: bool;
}

type transfer = {
  token_id: nat;
  new_owner: address;
}

type storage = {
  players: (address, game_data) map;
  nfts: (nat, nft) map;
  counter: nat;
  hints: string list;
}


type action =
  | StartGame of string
  | Guess of string
  | EndGame
  | MintNFT of nft
  | Transfer of transfer

let init_storage (_ : unit) : storage = {
  players = Map.empty;
  nfts = Map.empty;
  counter = 0n;
  hints = ["Hint 1"; "Hint 2"; "Hint 3"; "Hint 4"; "Hint 5"];
}

let main (p: action) (s: storage) : operation list * storage =
  match p with
  | StartGame hint ->
    let player = Tezos.sender in
    let new_game_data = {hint = hint; attempts = 2; time_started = Tezos.now; place_choice = ""; is_active = true} in
    let updated_players = Map.update player (Some new_game_data) s.players in
    ([], {s with players = updated_players})

  | Guess guess ->
    let player = Tezos.sender in
    let player_game_data = match Map.find_opt player s.players with
      | Some gd -> gd
      | None -> failwith "player not in game" in
    if player_game_data.attempts > 0n then
      let updated_game_data = {player_game_data with attempts = player_game_data.attempts - 1; place_choice = guess} in
      let updated_players = Map.update player (Some updated_game_data) s.players in
      ([], {s with players = updated_players})
    else ([], s)


  | EndGame ->
    let player = Tezos.sender in
    let updated_players = Map.remove player s.players in
    ([], {s with players = updated_players})

  | MintNFT new_nft ->
    let updated_nfts = Map.update new_nft.token_id (Some new_nft) s.nfts in
    ([], {s with nfts = updated_nfts})

  | Transfer transfer_data ->
    let nft_data = match Map.find_opt transfer_data.token_id s.nfts with
      | Some nft -> nft
      | None -> failwith "NFT does not exist" in
    let updated_nft = {nft_data with owner = transfer_data.new_owner} in
    let updated_nfts = Map.update transfer_data.token_id (Some updated_nft) s.nfts in
    ([], {s with nfts = updated_nfts})
