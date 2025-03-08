interface ITeam {
  id: number,
  name: string,
  city: string
}

interface IPlayer {
  firstName: string,
  lastName: string
}

const getPlayers = async () => {
  const res = await fetch(`${process.env.PLAYER_API}/players`, {cache: 'no-store'});
  return await res.json() as IPlayer[];
} 

const getTeams = async () => {
  const res = await fetch(`${process.env.TEAM_API}/teams`, {cache: 'no-store'});
  return await res.json() as ITeam[];
}

export default async function Page() {
  const teams = await getTeams();
  const players = await getPlayers();

  return (
    <div className="p-10">
      <p className="text-3xl font-bold text-center">Aspire Demo</p>
      <div className="flex gap-20 p-10 justify-center">
        <Teams teams={teams}/>
        <Players players={players}/>
      </div>
    </div>
  );
}

function Teams(props: {teams: ITeam[]}) {
  return (
    <div>
      <p className="text-xl font-bold underline mb-1">Teams</p>

      <table className="text-left">
        <tr className="text-lg">
          <th>Name</th>
          <th>City</th>
        </tr>
        {props.teams.map((team, i) => 
          <tr key={i}>
            <td className="pr-5">{team.name}</td>
            <td>{team.city}</td>
          </tr>
        )}
      </table>
      
    </div>
  )
}

function Players(props: {players: IPlayer[]}) {
  return (
    <div>
      <p className="text-xl font-bold underline mb-1">Players</p>

      <table className="text-left">
        <tr className="text-lg">
          <th>Name</th>
        </tr>
        {props.players.map((player, i) => 
          <tr key={i}>
            <td className="pr-5">{player.firstName} {player.lastName}</td>
          </tr>
        )}
      </table>
    </div>
  )
}