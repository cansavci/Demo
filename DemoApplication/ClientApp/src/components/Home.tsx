import * as React from "react";
import { connect } from "react-redux";
import { Container, Box, Button } from "@material-ui/core";
import DemoPage from "./Demo";

const Home = () => {
  return (
    <div>
      <Box>
        <input
          accept="image/*"
          id="contained-button-file"
          multiple
          type="file"
        />
        <label htmlFor="contained-button-file">
          <Button variant="contained" color="primary" component="span">
            Upload
          </Button>
        </label>
      </Box>
      <DemoPage />
    </div>
  );
};

export default connect()(Home);
