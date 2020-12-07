import React, { useState, useEffect } from "react";
const $ = window.$;

const RegionChoice = (props) => {
  return (
    <>
      <label className="radioLabels">
        <input
          type="radio"
          name={props.name}
          value={props.value}
          checked={props.checked}
          onChange={() => props.handleChange(props.value)}
        />
        {props.value}
      </label>
    </>
  );
};

const RegionForm = (props) => {
  const [selected, setSelected] = useState("All");

  function handleChange(value) {
    setSelected(value);
    props.handleOptionChange(value);
  }

  const data = [
    { id: 0, value: "All" },
    { id: 1, value: "Americas" },
    { id: 2, value: "APAC" },
    { id: 3, value: "EMEA" },
    { id: 4, value: "India" },
    { id: 5, value: "Japan" },
  ];

  const items = data.map((each) => (
    <RegionChoice
      name="regions"
      value={each.value}
      handleChange={handleChange}
      key={each.id}
      checked={each.value === selected}
    />
  ));

  return <form>{items}</form>;
};

export const Filters = (props) => {
  const [view, setView] = useState(null);
  const [_, setOption] = useState("All");
  // const revealEl = useRef(null);

  function handleOptionChange(value) {
    setOption(value);
    const filterSelectedValue = value === "All" ? [] : [value];
    view.dashboard.filters[0].selectedValues = filterSelectedValue;
  }

  function handleDataPointClick(widget, cell, row) {
    console.log(widget);
    console.log(cell);
    console.log(row);
  }

  // Setup reveal dashboard
  const initRevealBoard = () => {
    const dashboardId = "Sales";

    $.ig.RevealUtility.loadDashboard(
      dashboardId,
      (dashboard) => {
        var v = new $.ig.RevealView("#sales");
        v.onVisualizationDataPointClicked = handleDataPointClick;
        v.dashboard = dashboard;

        setView(v);
      },
      (error) => console.log(error)
    );
  };

  useEffect(() => {
    initRevealBoard();
  }, []);

  return (
    <div>
      <RegionForm handleOptionChange={handleOptionChange} />
      <div id="sales" />
    </div>
  );
};
