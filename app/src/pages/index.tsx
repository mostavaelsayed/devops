import { useEffect, useState } from "react";

export default function Home() {
  const [data, setData] = useState<any[]>([]);
  const getData = async () => {
    const response = await fetch(
      "http://localhost:5000/WeatherForecast"
    ).then((res) => res.json());
    setData(response);
  };
  useEffect(() => {
    getData();
  }, []);
  return (
    <main
      className={`flex min-h-screen flex-col items-center justify-between p-24`}
    >
      <div className="z-10 max-w-5xl w-full items-center justify-between font-mono text-sm lg:flex">
        <p className="fixed left-0 top-0 flex w-full justify-center border-b border-gray-300 bg-gradient-to-b from-zinc-200 pb-6 pt-8 backdrop-blur-2xl dark:border-neutral-800 dark:bg-zinc-800/30 dark:from-inherit lg:static lg:w-auto  lg:rounded-xl lg:border lg:bg-gray-200 lg:p-4 lg:dark:bg-zinc-800/30">
          Weather Forecast &nbsp;
        </p>
        <table>
          <thead>
            <tr>
              <th>temperatureF</th>
              <th>temperatureC</th>
              <th>summary</th>
              <th>date</th>
            </tr>
          </thead>
          <tbody>
            {data.map((row) => (
              <tr key={row.date}>
                <td>{row.temperatureF}</td>
                <td>{row.temperatureC}</td>
                <td>{row.summary}</td>
                <td>{row.date.toString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </main>
  );
}
