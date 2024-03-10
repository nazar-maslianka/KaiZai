// eslint-disable-next-line no-unused-vars
import React from "react";
import {
  Item,
  ItemGroup,
} from "semantic-ui-react";
import { usePaginatedIncomes } from "../../app/hooks/usePaginatedIncomes"

export default function IncomesItems() {
  const { paginatedIncomes } = usePaginatedIncomes();

  const renderedIncomes = paginatedIncomes && paginatedIncomes.map(income =>

    <Item key={income.id}>
      <Item.Image size='small' />
      <Item.Header>{income.incomeDate}</Item.Header>
      <Item.Extra>{income.amount}</Item.Extra>
    </Item>
  );

  return (
    <>
      <ItemGroup>{renderedIncomes}</ItemGroup>
    </>
  );
}