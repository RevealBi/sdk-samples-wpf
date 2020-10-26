import React, { useEffect } from "react";
const $ = window.$;

export const Linking = (props) => {
  function handleLinkingDashboard(title, url, callback) {
    console.log("Link followed - " + title);
    console.log("Url - " + url);
    var dashboardId = "Campaigns";
    callback(dashboardId);
  }

  const initBoard = () => {
    const dashboardId = "Marketing";
    $.ig.RevealUtility.loadDashboard(
      dashboardId,
      (dashboard) => {
        var view = new $.ig.RevealView("#marketing");
        view.onVisualizationLinkingDashboard = handleLinkingDashboard;
        view.dashboard = dashboard;
      },
      (error) => console.log(error)
    );
  };

  useEffect(() => {
    initBoard();
  }, []);

  return <div id="marketing" />;
};
