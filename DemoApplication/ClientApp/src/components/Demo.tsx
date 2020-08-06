import * as React from "react";
import { Icon, Avatar } from "@material-ui/core";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import * as DemoStore from '../store/Demo';
import { RouteComponentProps } from "react-router";
import { ApplicationState2 } from "../store/index2";
import { connect } from "react-redux";
import MaterialTable from "material-table";

type DemoProps =
  DemoStore.AssetsVariantState // ... state we've requested from the Redux store
  & typeof DemoStore.actionCreators // ... plus action creators we've requested
  & RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters

const DemoPage: React.FunctionComponent<DemoProps> = props => {

  const [assetData, setAssetData] = React.useState([]);
  const [folderData, setFolderData] = React.useState([]);
  const [variantsData, setVariantsData] = React.useState([]);

  React.useEffect( () => {
    props.requestAssets();
    setAssetData(props.assets as any);
  }, [] )

  const columns = [
    { title: "id", field: "id", hidden: true },
    { title: "Name", field: "name" },
    { title: "folderId", field: "folderId" }
  ];

return (
  <React.Fragment>
    <Icon>Can</Icon>
    <MaterialTable title="Sample table" columns={columns} data={assetData} />
  </React.Fragment>
);
};

export default connect(
  (state: ApplicationState2) => state, // Selects which state properties are merged into the component's props
  DemoStore.actionCreators // Selects which action creators are merged into the component's props
)(DemoPage as any);
