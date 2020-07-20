import React, { Component } from "react";

export class Linking extends Component {
  static displayName = Linking.name;

  constructor(props) {
    super(props);
    this.state = {};
    this.onVisualizationLinkingDashboard = this.onVisualizationLinkingDashboard.bind(
      this
    );
  }

  componentDidMount() {
    console.log(this.nv);

    this.rv.addEventListener(
      "onVisualizationLinkingDashboard",
      this.onVisualizationLinkingDashboard
    );
  }

  componentWillUnmount() {
    this.rv.removeEventListener(
      "onVisualizationLinkingDashboard",
      this.onVisualizationLinkingDashboard
    );
  }

  onVisualizationLinkingDashboard(event) {
    event.detail.callback("Campaigns");
  }

  render() {
    return (
      <reveal-view
        ref={(elem) => (this.rv = elem)}
        dashboard-name="Marketing"
        onVisualizationDataPointClicked={this.onVisualizationLinkingDashboard}
      ></reveal-view>
    );
  }
}
