interface ITeam {
  id: number,
  name: string,
  city: string
}

interface IPlayer {
  firstName: string,
  lastName: string
}

interface IPlayerTeamAggregation {
  player: IPlayer,
  team: ITeam
}

const getPlayerTeamAggregations = async () => {
  const res = await fetch(`${process.env.AGGREGATOR_API}/aggregate`, {cache: 'no-store'});
  return await res.json() as IPlayerTeamAggregation[];
} 

export default async function Page() {
  const playerTeamAggregations = await getPlayerTeamAggregations();

  return (
    <div className="p-10">
      <p className="text-3xl font-bold text-center">Aspire Demo</p>
      <div className="flex gap-20 p-10 justify-center">
        <table className="text-left">
          <tr className="text-lg">
            <th>Player</th>
            <th>Team</th>
          </tr>
          {playerTeamAggregations.map((pt, i) => (
            <tr key={i}>
              <td className="pr-5">{pt.player.firstName} {pt.player.lastName}</td>
              <td>{pt.team.name}</td>
            </tr>
          ))}
        </table>
      </div>
    </div>
  );
}
