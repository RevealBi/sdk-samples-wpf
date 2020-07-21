import React, { useState, useRef, useEffect } from "react";
const $ = window.$;


const CountryChoice = (props) => {
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



const CountryForm = (props) => {
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

  const items = data.map(each =>
    <CountryChoice
      name="counties"
      value={each.value}
      handleChange={handleChange}
      key={each.id}
      checked={each.value === selected}
    />
  );

  return (
    <form>
      {items}
    </form>
  );
};

export const Filters = (props) => {
  const [view, setView] = useState(null);
  const [_, setOption] = useState('All');
  const revealEl = useRef(null);

  function handleOptionChange(value) {
    setOption(value);
    const filter = value === 'All' ? [] : [value];
    view.setFilterSelectedValues(view.dashboard.filters()[0], filter);
  }

  function handleClick(event) {
    console.log(event);
  }

  // Setup reveal dashboard
  const initRevealBoard = () => {
    const dashboardId = "Sales";
    const settings = new $.ig.RevealSettings(dashboardId);
    settings.showFilters = true;

    $.ig.RevealUtility.loadDashboard(dashboardId, (dashboard) => {
      settings.dashboard = dashboard;
      setView(new $.ig.RevealView("#sales", settings));
    }, (error) => console.log(error));
  };

  // Setup event listeners and create dashboard
  useEffect(() => {
    initRevealBoard();
    const el = revealEl.current
    el.addEventListener('onVisualizationDataPointClicked', handleClick);
    return () => {
      el.removeEventListener('onVisualizationDataPointClicked', handleClick);
    };
  }, []);

  return (
    <div>
      <CountryForm handleOptionChange={handleOptionChange} />
      <div id="sales" ref={revealEl} />
    </div>
  );
};
