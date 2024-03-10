/* eslint-disable no-unused-vars */
// eslint-disable-next-line no-unused-vars
import React, { useState } from "react";

import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useDispatch, useSelector } from "react-redux";
import { setPageNumber } from "./incomesSlice";
import {
  Pagination, Divider, SegmentGroup, Segment, GridRow, GridColumn, Grid, ListItem,
  ListHeader,
  ListContent,
  Image,
  List,
} from 'semantic-ui-react';
import IncomesItems from "./IncomesItems";
import { format, subMonths, startOfMonth, isFirstDayOfMonth } from "date-fns";
import { PieChart, Pie, Sector, Cell, ResponsiveContainer } from 'recharts';

export default function IncomesTransactions() {
  const dispatch = useDispatch();
  //TODO: Test later
  const { pagingParams, filteringParams } = useSelector((state) => state.incomesDataViewSettings);
  const { startDate, endDate } = filteringParams;
  const { pageNumber, pageSize } = pagingParams;

  const [curPageSize, setCurPageSize] = useState(pageSize);
  const [curViewPage, setCurViewNextPage] = useState(pageNumber);
  const [curViewDateRange, setCurViewDateRange] = useState([new Date(startDate), new Date(endDate)]);
  const [curViewStartDate, curViewEndDate] = curViewDateRange;

  const handlePageChange = (event, data) => {
    setCurViewNextPage(data.activePage);
    dispatch(setPageNumber(data.activePage));
  };

  //TODO: convert date to string
  const handleDateRangeChange = (passedDateRangeArr) => {
    console.log(passedDateRangeArr);
    const objDateRan = { ...passedDateRangeArr };
    dispatch(setCurViewDateRange(objDateRan));
  };

  const data = [
    { name: 'Group A', value: 400 },
    { name: 'Group B', value: 300 },
    { name: 'Group C', value: 300 },
    { name: 'Group D', value: 200 },
  ];

  const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];

  const RADIAN = Math.PI / 180;
  const renderCustomizedLabel = ({ cx, cy, midAngle, innerRadius, outerRadius, percent }) => {
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const x = cx + radius * Math.cos(-midAngle * RADIAN);
    const y = cy + radius * Math.sin(-midAngle * RADIAN);

    return (
      <text x={x} y={y} fill="white" textAnchor={x > cx ? 'start' : 'end'} dominantBaseline="central">
        {`${(percent * 100).toFixed(0)}%`}
      </text>
    );
  };

  return (
    <Grid container>
      <GridRow columns={2} className="feature-content-section">
        <GridColumn className="centered-chart">
          <ResponsiveContainer>
          <PieChart >
            <Pie
              data={data}
              cx="50%"
              cy="50%"
                labelLine={false}
                
              label={renderCustomizedLabel}
              fill="#FF8042"
              dataKey="value"
            >
              {data.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
              ))}
            </Pie>
            </PieChart>
            </ResponsiveContainer>
        </GridColumn>
        <GridColumn
          verticalAlign="middle">
          <List
            size='medium'
            relaxed={{ relaxed:true }}
            className="chart-data-list scrolled-list" animated divided>
            <ListItem>
            <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Helen</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
            <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Christian</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
              <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Daniel</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
              <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Daniel</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
              <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Daniel</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
              <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Daniel</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
              <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Daniel</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
              <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Daniel</ListHeader>
              </ListContent>
            </ListItem>
            <ListItem>
              <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
              <ListContent>
                <ListHeader>Daniel</ListHeader>
              </ListContent>
            </ListItem>
          </List>
        </GridColumn>
      </GridRow>
      <Divider hidden />
      <GridRow className="feature-content-section">
        <GridColumn>
                {/* //TODO change style for background color */}
          <DatePicker
            showIcon
            selectsRange={true}
            startDate={curViewStartDate}
            endDate={curViewEndDate}
            onChange={(selectedDateRange) => {
              setCurViewDateRange(selectedDateRange);
              let isNotNullDate = dateValue => dateValue != null;
              if (selectedDateRange.every(isNotNullDate))
                handleDateRangeChange(selectedDateRange)
            }}
            isClearable={true}
          />
          {/* TODO add states of loading */}
          <IncomesItems />
          <Pagination
            style={{ background: '#8f48c7' }}
            onPageChange={handlePageChange}
            defaultActivePage={curViewPage}
            totalPages={pageSize} />
        </GridColumn>
      </GridRow>
    </Grid>
  );
}