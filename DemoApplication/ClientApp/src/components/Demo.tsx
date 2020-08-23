import * as React from "react";
import { Icon, Avatar } from "@material-ui/core";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import * as DemoStore from "../store/Demo";
import { RouteComponentProps } from "react-router";
import { ApplicationState } from "../store/index2";
import { connect } from "react-redux";
import MaterialTable, { Column } from "material-table";
import Upload from "./Upload";
import Axios from "axios";

type DemoProps = DemoStore.AssetsVariantState & // ... state we've requested from the Redux store
  typeof DemoStore.actionCreators & // ... plus action creators we've requested
  RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters

const DemoPage: React.FunctionComponent<DemoProps> = (props) => {
  const [assetData, setAssetData] = React.useState<DemoStore.Asset[]>([]);
  const [folderData, setFolderData] = React.useState<DemoStore.Folder[]>([]);
  const [variantsData, setVariantsData] = React.useState<DemoStore.Variant[]>(
    []
  );
  //for error handling
  const [iserror, setIserror] = React.useState(false);
  const [errorMessages, setErrorMessages] = React.useState<string[]>([]);

  React.useEffect(() => {
    props.requestAssets();
    //setAssetData({ ...props.assets });
    setAssetData([
      { id: 1, name: "aa", folderId: 1, variants: [] } as DemoStore.Asset,
    ]);
  }, []);

  const columns: any = [
    { title: "id", field: "id", hidden: true },
    { title: "Name", field: "name" },
    { title: "folderId", field: "folderId" },
  ];

  const handleRowAdd = (newData, resolve) => {
    Axios.post("/assets", newData)
      .then((res) => {
        let dataToAdd = [...data];
        dataToAdd.push(newData);
        setData(dataToAdd);
        resolve();
        setErrorMessages([]);
        setIserror(false);
      })
      .catch((error) => {
        setErrorMessages(["Cannot add data. Server error!"]);
        setIserror(true);
        resolve();
      });
  };

  const handleRowUpdate = (newData, oldData, resolve) => {
    Axios.patch("/assets/" + newData.id, newData)
      .then((res) => {
        const dataUpdate = [...data];
        const index = oldData.tableData.id;
        dataUpdate[index] = newData;
        setData([...dataUpdate]);
        resolve();
        setIserror(false);
        setErrorMessages([]);
      })
      .catch((error) => {
        setErrorMessages(["Update failed! Server error"]);
        setIserror(true);
        resolve();
      });
  };

  const handleRowDelete = (oldData, resolve) => {
    Axios.delete("/assets/" + oldData.id)
      .then((res) => {
        const dataDelete = [...data];
        const index = oldData.tableData.id;
        dataDelete.splice(index, 1);
        setData([...dataDelete]);
        resolve();
      })
      .catch((error) => {
        setErrorMessages(["Delete failed! Server error"]);
        setIserror(true);
        resolve();
      });
  };

  return (
    <React.Fragment>
      <Icon>Can</Icon>
      <MaterialTable
        title="Sample table"
        columns={columns}
        data={assetData}
        editable={{
          onRowUpdate: (newData, oldData) =>
            new Promise((resolve) => {
              handleRowUpdate(newData, oldData, resolve);
            }),
          onRowAdd: (newData) =>
            new Promise((resolve) => {
              handleRowAdd(newData, resolve);
            }),
          onRowDelete: (oldData) =>
            new Promise((resolve) => {
              handleRowDelete(oldData, resolve);
            }),
        }}
      />
      <Upload></Upload>
    </React.Fragment>
  );
};

export default connect(
  (state: ApplicationState) => state, // Selects which state properties are merged into the component's props
  DemoStore.actionCreators // Selects which action creators are merged into the component's props
)(DemoPage as any);
