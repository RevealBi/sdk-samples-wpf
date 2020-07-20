import React, { Component } from "react";
import $ from "jquery";

export class Filters extends Component {
  // static setState = Filters.name;

  constructor(props) {
    super(props);
    this.state = { selectedOption: "All" };
    this.handleDataPointClicked = this.handleDataPointClicked.bind(this);
    this.handleAmericas = this.handleAmericas.bind(this);
    this.handleOptionChange = this.handleOptionChange.bind(this);
  }

  componentDidMount() {
    this.rv = new $.ig.RevealView("revealView");
    console.log(this.nv);

    this.rv.addEventListener(
      "onVisualizationDataPointClicked",
      this.handleDataPointClicked
    );
  }

  componentWillUnmount() {
    // this.rv.removeEventListener(
    //   "onVisualizationDataPointClicked",
    //   this.handleDataPointClicked
    // );
  }

  handleDataPointClicked = (event) => {
    console.log(event);
  };

  handleAmericas = (event) => {
    //this.rv.dashboard.filters[0];
    this.rv.setFilterSelectedValues(this.rv.dashboard.filters()[0], [
      "Americas",
    ]);
    console.log(this.rv.dashboard.filters()[0]);
  };

  handleOptionChange = (event) => {
    this.setState({
      selectedOption: event.target.value,
    });

    let selectedFilterValues =
      event.target.value === "All" ? [] : [event.target.value];

    this.rv.setFilterSelectedValues(
      this.rv.dashboard.filters()[0],
      selectedFilterValues
    );
  };

  render() {
    return (
      <div>
        <span>
          <form>
            <label className="radioLabels">
              <input
                type="radio"
                value="All"
                checked={this.state.selectedOption === "All"}
                onChange={this.handleOptionChange}
              />
              All
            </label>

            <label className="radioLabels">
              <input
                type="radio"
                value="Americas"
                checked={this.state.selectedOption === "Americas"}
                onChange={this.handleOptionChange}
              />
              Americas
            </label>
            <label className="radioLabels">
              <input
                type="radio"
                value="APAC"
                checked={this.state.selectedOption === "APAC"}
                onChange={this.handleOptionChange}
              />
              APAC
            </label>
            <label className="radioLabels">
              <input
                type="radio"
                value="EMEA"
                checked={this.state.selectedOption === "EMEA"}
                onChange={this.handleOptionChange}
              />
              EMEA
            </label>
            <label className="radioLabels">
              <input
                type="radio"
                value="India"
                checked={this.state.selectedOption === "India"}
                onChange={this.handleOptionChange}
              />
              India
            </label>
            <label className="radioLabels">
              <input
                type="radio"
                value="Japan"
                checked={this.state.selectedOption === "Japan"}
                onChange={this.handleOptionChange}
              />
              Japan
            </label>
          </form>
        </span>
        <div id="revealView" />
      </div>
    );
  }
}
