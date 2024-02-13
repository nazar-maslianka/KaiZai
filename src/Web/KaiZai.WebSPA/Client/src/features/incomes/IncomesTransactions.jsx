/* eslint-disable no-unused-vars */
import agent from "../../app/api/agent.js";
import React, { useState } from "react";
import DatePicker from "react-datepicker";
import { useDispatch, useSelector } from "react-redux";
import {
    selectAllIncomes,
    useGetPaginatedIncomesQuery,
} from "./incomesSlice.js";
import {
    GridColumn,
    Button,
    Grid,
    Segment,
    Item,
    ItemGroup,
} from "semantic-ui-react";
import { Pagination } from 'semantic-ui-react';
import IncomesItems from "../../features/incomes/IncomesItems";

export default function IncomesTransactions() {


    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());


    return (
        <>
            <DatePicker
                selected={startDate}
                onChange={(date) => setStartDate(date)}
            />
            <DatePicker selected={endDate} onChange={(date) => setEndDate(date)} />
            <IncomesItems />
            <Pagination defaultActivePage={5} totalPages={10} />
        </>
    );
}