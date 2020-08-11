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
    const settings = new $.ig.RevealSettings(dashboardId);
    $.ig.RevealUtility.loadDashboard(
      dashboardId,
      (dashboard) => {
        settings.dashboard = dashboard;
        var view = new $.ig.RevealView("#marketing", settings);
        view.onVisualizationLinkingDashboard = handleLinkingDashboard;
      },
      (error) => console.log(error)
    );
  };

  useEffect(() => {
    initBoard();
  }, []);

  return <div id="marketing" />;
};
