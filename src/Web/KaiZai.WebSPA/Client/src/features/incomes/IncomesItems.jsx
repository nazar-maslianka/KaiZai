/* eslint-disable no-unused-vars */
import React, { useState } from "react";
import DatePicker from "react-datepicker";
import { useSelector } from "react-redux";
import {
  selectAllIncomes,
} from "./incomesSlice.js";
import {
  Item,
  ItemGroup,
} from "semantic-ui-react";

export default function IncomesItems() {
  const paginatedIncomes = useSelector(selectAllIncomes);

  const renderedIncomes = paginatedIncomes.map((income) => (
    <Item key={income.id}>
      <Item.Image size='small'/>
      <Item.Header>{income.incomeDate}</Item.Header>
      <Item.Description>{income.descrition}</Item.Description>
      <Item.Extra>{income.amount}</Item.Extra>
    </Item>
  ));

  return (
    <>
      <ItemGroup>{renderedIncomes}</ItemGroup>
    </>
  );
}
