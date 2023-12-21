import agent from "../../app/api/agent";

export default function IncomeCard() {
    const params = new URLSearchParams();
    params.append('pageNumber', '2');
    params.append('pageSize', '3');
    const res = agent.Incomes.list(params);
    console.log(res);
}