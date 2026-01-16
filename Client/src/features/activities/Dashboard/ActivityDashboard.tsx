import { Grid2 } from "@mui/material";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

type Props = {
    activities: Activity[];
    selectActivity: (id: string) => void;
    cancelSelectActivity: () => void;
    selectedActivity: Activity | undefined;
    editMode: boolean
    openForm: (id?: string) => void
    closeForm: () => void
    submitForm: (activity: Activity) => void
    deleteActivity: (id: string) => void
}

export default function ActivityDashboard({ activities,
    selectActivity,
    cancelSelectActivity,
    selectedActivity,
    editMode,
    openForm,
    closeForm,
    submitForm,
    deleteActivity }
    : Props) {
    return (
        <Grid2 container spacing={3}>
            <Grid2 size={7}>
                <ActivityList
                    activities={activities}
                    selectActivity={selectActivity}
                    deleteActivity={deleteActivity}
                />
            </Grid2>
            <Grid2 size={5}>
                {selectedActivity && !editMode &&
                    <ActivityDetails
                        activity={selectedActivity}
                        cancelSelectActivity={cancelSelectActivity}
                        openForm={openForm}
                    />
                }
                {editMode &&
                    <ActivityForm submitForm={submitForm} closeForm={closeForm} activity={selectedActivity}></ActivityForm>
                }
            </Grid2>
        </Grid2 >
    )
}
