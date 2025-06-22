import { defineStore } from "pinia";
import { reactive } from "vue";
import type { Column, Task } from "@/types";
import { v4 as uuidv4 } from "uuid";

export const useColumnStore = defineStore('column', () => {
    const uuid = uuidv4()
    const columns = reactive({
        [uuid]: {
            id: uuid,
            name: "Задания",
            tasks: [],
        }
    } as Record<string, Column>)
    
    const addColumn = (column: Column) => {
        columns[column.id] = column;
    }
    
    const pushTaskToColumn = (columnId: string, task: Task) => {
        columns[columnId].tasks.push(task);
    }
    
    return { columns, addColumn, pushTaskToColumn }
})